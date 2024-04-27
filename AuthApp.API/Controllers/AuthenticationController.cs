using AuthApp.API.Common;
using AuthApp.Core.Features.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.API.Controllers
{
    [Route("api/authapi/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator mediator) : base(mediator)
        {
            
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] PostLoginCommand command)
        {
            return Ok(APIResponse<PostLoginResponse>.Success(await _mediatR.Send(command)));
        }

        
    }
}
