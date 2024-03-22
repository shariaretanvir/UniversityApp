using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApp.API.Common;
using StudentApp.Core.Feature.Student.Post;
using StudentApp.Core.Feature.Student.Put;

namespace StudentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(IMediator mediator, ILogger<StudentController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpPost(Name = "SaveStudent")]
        public async Task<IActionResult> Post([FromBody] PostStudentCommand command)
        {
            return Ok(APIResponse<PostStudentResponse>.Success(await _mediator.Send(command), "Saved"));
        }

        [HttpPut(Name = "UpdateStudent")]
        public async Task<IActionResult> Put([FromBody] PutStudentCommand command)
        {
            return Ok(APIResponse<PutStudentResponse>.Success(await _mediator.Send(command), ""));
        }

        []
    }
}
