using AuthApp.Core.Infra;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Features.ApplicationUser.Post
{
    public class PostApplicationUserHandler(
        ILogger<PostApplicationUserHandler> _logger, IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<PostApplicationUserCommand, PostApplicationResponse>
    {
        public async Task<PostApplicationResponse> Handle(PostApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = _mapper.Map<Entities.ApplicationUser>(request);
            await _unitOfWork.GetRepository<Entities.ApplicationUser, Guid>().AddAsync(applicationUser);

            if(await _unitOfWork.CommitAsync() > 0)
            {
                return new PostApplicationResponse(true);
            }
            else
            {
                return new PostApplicationResponse(false);
            }
        }
    }
}
