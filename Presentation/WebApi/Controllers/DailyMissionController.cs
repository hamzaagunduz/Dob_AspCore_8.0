using Application.Features.Mediator.Commands.DailyMissionsCommands;
using Application.Features.Mediator.Queries.DailyMissionsQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyMissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DailyMissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateMission(UpdateDailyMissionCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { Message = "Görevler güncellendi." });
        }

        [HttpGet("missions/{userId}")]
        public async Task<IActionResult> GetUserDailyMissions(int userId)
        {
            var result = await _mediator.Send(new GetUserDailyMissionsQuery(userId));
            return Ok(result);
        }
    }

}
