using Application.Features.Mediator.Commands.ExamCommands;
using Application.Features.Mediator.Queries.ExamQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace WebApi.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ExamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateExam(CreateExamCommand command)
        {
            await _mediator.Send(command);
            return Ok("Exam successfully created.");
        }


        [AllowAnonymous] // Bu endpoint için authorization geçersiz
        [HttpGet]
        public async Task<IActionResult> GetAllExams()
        {
            var exams = await _mediator.Send(new GetAllExamQuery());
            return Ok(exams);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamById(int id)
        {
            var exam = await _mediator.Send(new GetExamByIdQuery( id));
            return Ok(exam);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExam([FromBody] UpdateExamCommand command)
        {
            await _mediator.Send(command);
            return Ok("Exam successfully Update.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveExam(int id)
        {
            await _mediator.Send(new RemoveExamCommand(id));
            return Ok("Exam successfully Remove.");
        }
        [HttpPut("select/{id}")]
        public async Task<IActionResult> SelectExam(int id)
        {
            // SelectExamCommand ile sınavı seç
            await _mediator.Send(new SelectExamCommand { ExamID = id });
            return Ok("Exam successfully selected.");
        }

    }
}

