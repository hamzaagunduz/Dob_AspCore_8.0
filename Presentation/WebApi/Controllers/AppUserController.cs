using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AppUserList()
        {
            var values = await _mediator.Send(new GetAllAppUserQuery());
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser(int id)
        {
            var value = await _mediator.Send(new GetAppUserByIdQuery(id));
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateAppUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok("AppUser başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAppUser(int id)
        {
            await _mediator.Send(new RemoveAppUserCommand(id));
            return Ok("AppUser başarıyla silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("AppUser başarıyla güncellendi");
        }


        [HttpPost("LoginToken")]
        public async Task<IActionResult> LoginToken(GetCheckAppUserQuery query)
        {
            var values = await _mediator.Send(query);
            if (values.IsExist)
            {
                return Created("", JwtTokenGenerator.GenerateToken(values));
            }
            else
            {
                return BadRequest("Kullanıcı adı veya şifre hatalıdır");
            }
        }

        [HttpGet("GetUserProfile")]

        public IActionResult GetUserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            return Ok(new { UserId = userId, Username = User.Identity.Name });
        }

        [HttpPut("UpdateUserExam")]
        public async Task<IActionResult> UpdateAppUserExam([FromBody] UpdateAppUserExamCommand command)
        {
            // Sınav ID'si ve kullanıcı ID'si validasyonları yapılabilir
            if (command.UserId <= 0 || command.ExamID <= 0)
            {
                return BadRequest("Geçersiz kullanıcı ID'si veya sınav ID'si.");
            }

            // Komutu MediatR üzerinden işleme gönderiyoruz
            await _mediator.Send(command);
            return Ok("Kullanıcının sınavı başarıyla güncellendi.");
        }

        [HttpPost("decreaselife/{userId}")]
        public async Task<IActionResult> DecreaseLife(int userId)
        {
            var result = await _mediator.Send(new DecreaseLifeCommand(userId));
            if (!result)
                return BadRequest("User not found or no lives left.");

            return Ok("Life decreased successfully.");
        }

        [HttpGet("lives/{userId}")]
        public async Task<IActionResult> GetLivesInfo(int userId)
        {
            var result = await _mediator.Send(new GetHealtByUserIdQuery(userId));
            if (result == null)
                return NotFound("User not found.");

            return Ok(result);
        }

        [HttpPost("purchase-diamonds")]
        public async Task<IActionResult> PurchaseDiamonds([FromBody] PurchaseDiamondCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest("Purchase failed.");

            return Ok("Purchase successful.");
        }

    }
}
