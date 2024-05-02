using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApp.API.Common;
using StudentApp.Core.Common;
using StudentApp.Core.Feature.Student.Delete;
using StudentApp.Core.Feature.Student.Get;
using StudentApp.Core.Feature.Student.Post;
using StudentApp.Core.Feature.Student.Put;

namespace StudentApp.API.Controllers
{
    [Authorize]
    [Route("api/studentapi/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(IMediator mediator, ILogger<StudentController> logger) : base(mediator)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("SaveStudent")]
        public async Task<IActionResult> Post([FromBody] PostStudentCommand command)
        {
            return Ok(APIResponse<PostStudentResponse>.Success(await _mediator.Send(command), "Saved"));
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> Put([FromBody] PutStudentCommand command)
        {
            return Ok(APIResponse<PutStudentResponse>.Success(await _mediator.Send(command), ""));
        }

        [HttpDelete]
        [Route("DeleteStudent")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(APIResponse<DeleteStudentResponse>.Success(await _mediator.Send(new DeleteStudentCommand { Id = id }), ""));
        }

        [HttpGet]
        [Route("GetStudents")]
        public async Task<IActionResult> Get([FromQuery] ResourceParameters resourceParameters)
        {
            return Ok(APIResponse<GetStudentResponse>.Success(await _mediator.Send(new GetStudentRequest { ResourceParameters = resourceParameters }), ""));
        }
    }
}
