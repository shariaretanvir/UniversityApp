using AuthApp.API.Common;
using AuthApp.Core.Features.ApplicationUser.Post;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.API.Controllers
{
    [Route("api/authapi/[controller]")]
    [ApiController]
    public class ApplicationUserController : BaseController
    {
        public ApplicationUserController(IMediator mediator) : base(mediator) { }
        
        [HttpPost]
        [Route("SaveApplicationUser")]
        public async Task<IActionResult> Post([FromBody] PostApplicationUserCommand command)
        {
            return Ok(APIResponse<PostApplicationResponse>.Success(await _mediatR.Send(command)));
        }
    }
}
