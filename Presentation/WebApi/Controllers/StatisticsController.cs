using Application.Features.Mediator.Commands.StatisticsCommands;
using Application.Features.Mediator.Queries.StatisticsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]

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
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

            command.AppUserId = userId; // Client'tan gelen ID'yi ez

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserStatistics()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

            var result = await _mediator.Send(new GetUserProfileStatisticsQuery(userId));

            if (result == null)
                return NotFound("Kullanıcı bulunamadı veya istatistik verisi yok.");

            return Ok(result);
        }

    }

}
