using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using StudentApp.API.Common;

namespace StudentApp.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //protected IActionResult OK(int statusCode = 200)
        //{
        //    return Ok(new APIResponse<string>(statusCode, "", null));
        //}
        //protected IActionResult OK(string message, int statudCode = 200)
        //{
        //    return Ok(new APIResponse<string>(statudCode, message, null));
        //}
        //protected IActionResult OK<T>(T data, string message, int statudCode = 200)
        //{
        //    return Ok(new APIResponse<T>(statudCode, message, data));
        //}

        //protected IActionResult Error<T>(T data, string message, int statusCode = 500)
        //{
        //    return BadRequest(new APIResponse<T>(statusCode, message, data));
        //}
    }
}
