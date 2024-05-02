using AuthApp.Core.Common;
using AuthApp.Core.Infra;
using AuthApp.Core.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Features.Authentication.RefreshToken
{
    public class PostRefreshTokenHandler : IRequestHandler<PostRefreshTokenCommand, PostRefreshTokenResponse>
    {
        private readonly ILogger<PostRefreshTokenHandler> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public PostRefreshTokenHandler(ILogger<PostRefreshTokenHandler> logger, IConfiguration configuration, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _logger = logger;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }
        public async Task<PostRefreshTokenResponse> Handle(PostRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new PostRefreshTokenResponse(false, "", "");
            }

            var tokenPrinciple = GetPrincipleFromToken(request.AccessToken);
            if (tokenPrinciple == null)
            {
                throw new Exception("Invalid token found");
            }
            var userName = tokenPrinciple.FindFirst(ClaimTypes.Name)?.Value;
            var email = tokenPrinciple.FindFirst(ClaimTypes.Email)?.Value;

            Entities.ApplicationUser user = GetUserFromDb(userName, email);
            if (user == null)
            {
                throw new Exception("Invalid user");
            }

            var userToken = _unitOfWork.GetRepository<Entities.UserToken, Guid>().Query(x => x.ApplicationUser).FirstOrDefault(x => x.UserId == user.Id && x.IsActive == true);

            if (userToken == null || userToken.RefreshToken != request.RefreshToken || userToken.RefreshTokenExpiryDateTime <= StaticDeclaration.GetActualBstTime(DateTime.UtcNow))
            {
                throw new Exception("Token not matched or expired");
            }

            var newToken = _tokenService.CreateToken(user);
            var newRefreshToken = _tokenService.CreateRefreshToken();

            userToken.SetRefreshToken(newRefreshToken);

            DateTime calculatedDateTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWTSettings:RefreshTokenValidityInDays"]));

            userToken.SetRefreshTokenExpiry(StaticDeclaration.GetActualBstTime(calculatedDateTime));

            _unitOfWork.GetRepository<Entities.UserToken, Guid>().Update(userToken);

            if (await _unitOfWork.CommitAsync() > 0)
            {
                return new PostRefreshTokenResponse(true, newToken, newRefreshToken);
            }
            else
            {
                return new PostRefreshTokenResponse(false, "", "");
            }
        }

        private Entities.ApplicationUser GetUserFromDb(string userName, string email)
        {
            return _unitOfWork.GetRepository<Entities.ApplicationUser, Guid>().Query().FirstOrDefault(x => x.UserName.Trim().ToLower() == userName.Trim().ToLower() && x.Email.Trim().ToLower() == email.Trim().ToLower() && x.IsActive == true);
        }

        private ClaimsPrincipal? GetPrincipleFromToken(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JWTSettings:Issuer"],
                ValidAudience = _configuration["JWTSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");

            return principle;
        }
    }
}
