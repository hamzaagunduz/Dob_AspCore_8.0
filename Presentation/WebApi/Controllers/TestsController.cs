using Application.Features.Mediator.Commands.TestCommand;
using Application.Features.Mediator.Queries.TestQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]

    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(CreateTestCommand command)
        {
            await _mediator.Send(command);
            return Ok("Test successfully created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            var tests = await _mediator.Send(new GetAllTestQuery());
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var test = await _mediator.Send(new GetTestByIdQuery(id));
            return Ok(test);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTest([FromBody] UpdateTestCommand command)
        {
            await _mediator.Send(command);
            return Ok("Test successfully updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTest(int id)
        {
            await _mediator.Send(new RemoveTestCommand(id));
            return Ok("Test successfully removed.");
        }
    }
}
