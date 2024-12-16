using Application.Features.Mediator.Commands.FlashCardCommands;
using Application.Features.Mediator.Queries.FlashCardQueries;
using Application.Features.Mediator.Queries.FlashCardQuery;
using MediatR;
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
    }
}
