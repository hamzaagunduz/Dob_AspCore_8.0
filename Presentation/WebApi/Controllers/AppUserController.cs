using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Tools;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentAppUser()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int id))
                return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

            var value = await _mediator.Send(new GetAppUserByIdQuery(id));
            return Ok(value);
        }

        [AllowAnonymous] // Bu endpoint için authorization geçersiz

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

        [AllowAnonymous] // Bu endpoint için authorization geçersiz
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
            // Token'dan userId al
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

            // Command'in UserId alanını token'dan gelen userId ile değiştir
            command.UserId = userId;

            // Sınav ID'si validasyonu yap
            if (command.ExamID <= 0)
            {
                return BadRequest("Geçersiz sınav ID'si.");
            }

            // Komutu MediatR ile işle
            await _mediator.Send(command);

            return Ok("Kullanıcının sınavı başarıyla güncellendi.");
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Geçerli bir kullanıcı bulunamadı.");

            command.AppUserId = userId;

            var result = await _mediator.Send(command);
            if (result)
                return Ok(new { message = "Şifre başarıyla değiştirildi." });

            return BadRequest(new { error = "Şifre değiştirme işlemi başarısız oldu. Eski şifre yanlış olabilir." });
        }


        [HttpPost("decreaselife")]
        public async Task<IActionResult> DecreaseLife()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Geçerli bir kullanıcı bulunamadı.");

            var result = await _mediator.Send(new DecreaseLifeCommand(userId));

            if (!result)
                return BadRequest("User not found or no lives left.");

            return Ok("Life decreased successfully.");
        }


        [HttpGet("lives")]
        public async Task<IActionResult> GetLivesInfo()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Geçerli bir kullanıcı bulunamadı.");

            var result = await _mediator.Send(new GetHealtByUserIdQuery(userId));

            if (result == null)
                return NotFound("User not found.");

            return Ok(result);
        }


        [HttpPost("purchase-diamonds")]
        public async Task<IActionResult> PurchaseDiamonds([FromBody] PurchaseDiamondCommand command)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized(new { error = "Geçersiz token veya kullanıcı bulunamadı." });

            command.UserId = userId; // Token'dan gelen ID ile üzerine yaz
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest("Purchase failed.");

            return Ok("Purchase successful.");
        }

        [HttpPut("ban")]
        public async Task<IActionResult> BanUser([FromBody] BanUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginatedUsers(
     [FromQuery] int pageNumber = 1,
     [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetAppUsersWithPaginationQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            return Ok(result);
        }


    }
}
