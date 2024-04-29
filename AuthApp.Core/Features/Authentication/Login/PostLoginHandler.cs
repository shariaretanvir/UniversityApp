using AuthApp.Core.Common;
using AuthApp.Core.Infra;
using AuthApp.Core.Services;
using AuthApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Features.Authentication.Login
{
    public class PostLoginHandler : IRequestHandler<PostLoginCommand, PostLoginResponse>
    {
        private readonly IRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public PostLoginHandler(IRepository repository, ITokenService tokenService, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _tokenService = tokenService;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<PostLoginResponse> Handle(PostLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetLoggedInUser(request.UserName, request.Email, request.Password);
            if (user == null)
            {
                return new PostLoginResponse(false, "", "");
            }
            else
            {
                var token = _tokenService.CreateToken(user);
                var refreshToken = _tokenService.CreateRefreshToken();
                
                DateTime calculatedDateTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWTSettings:RefreshTokenValidityInDays"]));
                
                var refreshTokenExpiry = StaticDeclaration.GetActualBstTime(calculatedDateTime); ;

                var existingUserToken = _unitOfWork.GetRepository<Entities.UserToken, Guid>().Query().Where(x => x.UserId == user.Id && x.IsActive == true).FirstOrDefault();

                if (existingUserToken is null)
                {
                    var userToken = new UserToken(Guid.NewGuid(), user.Id, refreshToken, refreshTokenExpiry, true);
                    await _unitOfWork.GetRepository<Entities.UserToken, Guid>().AddAsync(userToken);
                }
                else
                {
                    existingUserToken.SetRefreshToken(refreshToken);
                    existingUserToken.SetRefreshTokenExpiry(refreshTokenExpiry);
                    _unitOfWork.GetRepository<Entities.UserToken, Guid>().Update(existingUserToken);
                }

                if (await _unitOfWork.CommitAsync() > 0)
                {
                    return new PostLoginResponse(true, token, refreshToken);
                }
                return new PostLoginResponse(false, "", "");
            }
        }
    }
}
