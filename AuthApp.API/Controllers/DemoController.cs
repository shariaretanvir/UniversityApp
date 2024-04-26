using AuthApp.API.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.API.Controllers
{
    [Route("api/authapi/Demo")]
    [ApiController]
    public class DemoController : BaseController
    {
        public DemoController(IMediator mediator) : base(mediator)
        {

        }

        [Authorize(Roles = "Admin, Tester")]
        //[TypeFilter(typeof(RoleAuthorizationFilter), Arguments = new object[] { "Role", "Admin" })]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Success");
        }
    }
}
