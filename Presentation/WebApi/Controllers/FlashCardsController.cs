using Application.Features.Mediator.Commands.FlashCardCommands;
using Application.Features.Mediator.Queries.FlashCardQueries;
using Application.Features.Mediator.Queries.FlashCardQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class FlashCardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlashCardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlashCard(CreateFlashCardCommand command)
        {
            await _mediator.Send(command);
            return Ok("FlashCard successfully created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlashCards()
        {
            var result = await _mediator.Send(new GetAllFlashCardQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlashCardById(int id)
        {
            var result = await _mediator.Send(new GetFlashCardByIdQuery(id));
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlashCard(UpdateFlashCardCommand command)
        {
            await _mediator.Send(command);
            return Ok("FlashCard successfully updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFlashCard(int id)
        {
            await _mediator.Send(new RemoveFlashCardCommand(id));
            return Ok("FlashCard successfully deleted.");
        }

        [HttpGet("GetFlashCardsByQuestionId/{questionId}")]
        public async Task<IActionResult> GetFlashCardsByQuestionId(int questionId)
        {
            var query = new GetFlashCardsByQuestionIdQuery(questionId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetFlashCardsByTestId/{testId}")]
        public async Task<IActionResult> GetFlashCardsByTestId(int testId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

            var query = new GetFlashCardsByTestIdQuery(testId, userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPost("ToggleUserFlashCard")]
        public async Task<IActionResult> ToggleUserFlashCard([FromBody] AddUserFlashCardCommand command)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

            command.AppUserID = userId; // Client’tan gelen ID’yi override ediyoruz

            await _mediator.Send(command);
            return Ok("Favori flashcard başarıyla güncellendi.");
        }


        [HttpGet("IsFavorite")]
        public async Task<IActionResult> IsFavorite([FromQuery] int userId, [FromQuery] int flashCardId)
        {
            var result = await _mediator.Send(new IsFlashCardFavoriteQuery(userId, flashCardId));
            return Ok(result); // true veya false
        }

        [HttpGet("favorites/bycourse")]
        public async Task<IActionResult> GetUserFavoriteFlashCardsByCourse([FromQuery] int courseId)
        {
            // Token'dan userId al
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int appUserId))
                return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

            // Query objesini token'dan alınan userId ve gelen courseId ile oluştur
            var query = new GetUserFavoriteFlashCardsByCourseQuery(appUserId, courseId);

            // MediatR ile işle
            var result = await _mediator.Send(query);

            return Ok(result);
        }


    }
}
