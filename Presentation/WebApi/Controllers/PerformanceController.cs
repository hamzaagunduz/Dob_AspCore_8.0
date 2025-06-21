using Application.Features.Mediator.Commands.PerformanceCommands;
using Application.Features.Mediator.Queries.PerformanceQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

            command.AppUserId = userId; // Token'dan gelen ID ile override et

            await _mediator.Send(command);
            return Ok(new { message = "Performans verileri başarıyla kaydedildi." });
        }


        [HttpGet("performance")] 
        public async Task<IActionResult> GetPerformance([FromQuery] string range)
        {
            // Token'dan userId alıyoruz
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid token or user not found.");

            var result = await _mediator.Send(new PerformanceQuery
            {
                userId = userId,
                Range = range // "weekly", "monthly", "all"
            });

            return Ok(result);
        }

    }
}
