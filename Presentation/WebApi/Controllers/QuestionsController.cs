using Application.Features.Mediator.Commands.QuestionCommands;
using Application.Features.Mediator.Queries.QuestionQueries;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]

    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionCommand command)
        {
            await _mediator.Send(command);
            return Ok("Question successfully created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var result = await _mediator.Send(new GetAllQuestionQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var result = await _mediator.Send(new GetQuestionByIdQuery(id));
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuestion(UpdateQuestionCommand command)
        {
            await _mediator.Send(command);
            return Ok("Question successfully updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveQuestion(int id)
        {
            await _mediator.Send(new RemoveQuestionCommand(id));
            return Ok("Question successfully deleted.");
        }

        [HttpGet("GetQuestionsByTestId/{testId}")]
        public async Task<IActionResult> GetQuestionsByTestId(int testId)
        {
            var query = new GetQuestionsByTestIdQuery(testId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadQuestionImage([FromForm] int questionId, [FromForm] QuestionImageType type,  IFormFile file)

        {
            if (file == null || file.Length == 0)
                return BadRequest("Geçerli bir dosya yükleyin.");

            var command = new CreateQuestionImageCommand
            {
                QuestionID = questionId,
                Type = type,
                File = file
            };

            var imagePath = await _mediator.Send(command);
            return Ok(new { imageUrl = imagePath });
        }

        [HttpPost("create-full-question")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CreateFullQuestion([FromForm] CreateFullQuestionCommand command)
        {
            var questionId = await _mediator.Send(command);
            return Ok(questionId);
        }


    }
}
