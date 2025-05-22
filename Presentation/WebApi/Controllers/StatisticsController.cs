using Application.Features.Mediator.Commands.StatisticsCommands;
using Application.Features.Mediator.Queries.StatisticsQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateStatistics([FromBody] UpdateUserStatisticsCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserStatistics(int userId)
        {
            var result = await _mediator.Send(new GetUserProfileStatisticsQuery(userId));

            if (result == null)
                return NotFound("Kullanıcı bulunamadı veya istatistik verisi yok.");

            return Ok(result);
        }
    }

}
