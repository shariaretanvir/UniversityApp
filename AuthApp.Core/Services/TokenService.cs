using AuthApp.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Services
{
    public class TokenService : ITokenService
    {
        public IConfiguration Configuration { get; }

        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string CreateToken(ApplicationUser applicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Configuration["JWTSettings:SecretKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, applicationUser.Email),
                new Claim(ClaimTypes.Name, applicationUser.UserName)                
            };
            if(applicationUser.UserName == "Akash")
            {
                claims.Add(new Claim(ClaimTypes.Role, string.Join(",", "Admin")));
            }
            TimeZoneInfo bstTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"); // BST timezone
            DateTime calculatedDateTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(Configuration["JWTSettings:AccessTokenValidityInMinutes"]));
            DateTime bstDateTime = TimeZoneInfo.ConvertTimeFromUtc(calculatedDateTime, bstTimeZone);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = bstDateTime,
                Issuer = Configuration["JWTSettings:Issuer"],
                Audience = Configuration["JWTSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
