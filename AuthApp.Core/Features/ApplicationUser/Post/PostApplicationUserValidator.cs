using AuthApp.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Features.ApplicationUser.Post
{
    public class PostApplicationUserValidator : AbstractValidator<PostApplicationUserCommand>
    {
        public PostApplicationUserValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty().NotNull().MinimumLength(4);
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(x=>x.Password).NotEmpty().NotNull().MinimumLength(4);
        }
    }
}
