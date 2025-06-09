using Application.Features.Mediator.Commands.TestCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTestGroupCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { id });
        }

        [HttpPut]
        public async Task<IActionResult> Update( [FromBody] UpdateTestGroupCommand command)
        {
  

            var success = await _mediator.Send(command);
            if (!success)
                return NotFound("Test group not found.");

            return NoContent();
        }
    }
}
