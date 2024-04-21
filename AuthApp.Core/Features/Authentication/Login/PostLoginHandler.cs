using AuthApp.Core.Services;
using MediatR;
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

        public PostLoginHandler(IRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }
        public async Task<PostLoginResponse> Handle(PostLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetLoggedInUser(request.UserName, request.Email, request.Password);
            if (user == null)
            {
                return new PostLoginResponse(false, "");
            }
            else
            {
                var token = _tokenService.CreateToken(user);
                return new PostLoginResponse(true, token);
            }
        }
    }
}
