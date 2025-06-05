using Application.Features.Mediator.Commands.DailyMissionsCommands;
using Application.Features.Mediator.Queries.DailyMissionsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Geçerli bir kullanıcı bulunamadı.");

            command.AppUserId = userId;

            await _mediator.Send(command);
            return Ok(new { Message = "Görevler güncellendi." });
        }


        [HttpGet("missions")]
        public async Task<IActionResult> GetUserDailyMissions()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Geçerli bir kullanıcı bulunamadı.");

            var result = await _mediator.Send(new GetUserDailyMissionsQuery(userId));
            return Ok(result);
        }

    }

}
