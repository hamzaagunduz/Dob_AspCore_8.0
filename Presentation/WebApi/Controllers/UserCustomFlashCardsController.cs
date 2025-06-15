using Application.Features.Mediator.Commands.FlashCardCommands;
using Application.Features.Mediator.Queries.FlashCardQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCustomFlashCardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserCustomFlashCardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Belirli bir kullanıcıya ait özel kartları getir
        [HttpGet]
        public async Task<IActionResult> GetByUser([FromQuery] int userId)
        {
            var query = new GetUserCustomFlashCardsQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // Yeni özel kart oluştur
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCustomFlashCardCommand command)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");
            var updatedCommand = new CreateUserCustomFlashCardCommand(
                command.Front,
                command.Back,
                userId,           // Tokendan alınan kullanıcı ID
                command.CourseID
            );

            await _mediator.Send(updatedCommand);

            return Ok(new
            {
                message = "Özel kart başarıyla eklendi."
            });
        }


        // Özel kart güncelle
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCustomFlashCardCommand command)
        {

            await _mediator.Send(command);
            return Ok(new
            {
                message = "Özel kart başarıyla güncellendi."
            });
        }

        // Özel kart sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteUserCustomFlashCardCommand(id));
            return Ok(new
            {
                message = "Özel kart başarıyla silindi."
            });
        }
    }
}
