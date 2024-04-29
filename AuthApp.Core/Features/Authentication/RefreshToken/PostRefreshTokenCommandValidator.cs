using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Features.Authentication.RefreshToken
{
    public class PostRefreshTokenCommandValidator : AbstractValidator<PostRefreshTokenCommand>
    {
        public PostRefreshTokenCommandValidator() 
        {
            RuleFor(x=>x.AccessToken).NotEmpty().NotNull();
            RuleFor(x => x.RefreshToken).NotEmpty().NotNull();
        }
    }
}
