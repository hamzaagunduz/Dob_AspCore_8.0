using Application.Features.Mediator.Commands.ExamCommands;
using Application.Features.Mediator.Queries.ExamQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            await _mediator.Send(new RemoveExamCommand(id));
            return NoContent();
        }


    }
}

