using Application.Features.Mediator.Commands.PerformanceCommands;
using Application.Features.Mediator.Queries.PerformanceQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PerformanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitPerformance([FromBody] PerformanceCommand command)
        {
            if (command == null || command.Performances == null || !command.Performances.Any())
            {
                return BadRequest("Veri eksik veya hatalı.");
            }

            await _mediator.Send(command);
            return Ok(new { message = "Performans verileri başarıyla kaydedildi." });
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPerformance(int userId, [FromQuery] string range)
        {
            var result = await _mediator.Send(new PerformanceQuery
            {
                userId = userId,
                Range = range // "weekly", "monthly", "all"
            });

            return Ok(result);
        }
    }
}
