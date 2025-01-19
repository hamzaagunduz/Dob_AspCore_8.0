using Application.Features.Mediator.Commands.TopicCommands;
using Application.Features.Mediator.Queries.TopicQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TopicsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic(CreateTopicCommand command)
        {
            await _mediator.Send(command);
            return Ok("Topic successfully created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            var topics = await _mediator.Send(new GetAllTopicQuery());
            return Ok(topics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopicById(int id)
        {
            var topic = await _mediator.Send(new GetTopicByIdQuery(id));
            return Ok(topic);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTopic([FromBody] UpdateTopicCommand command)
        {
            await _mediator.Send(command);
            return Ok("Topic successfully updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTopic(int id)
        {
            await _mediator.Send(new RemoveTopicCommand(id));
            return Ok("Topic successfully removed.");
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetTopicsWithTestsByCourseId(int courseId)
        {
            var topicsWithTests = await _mediator.Send(new GetTopicsWithTestsByCourseIdQuery(courseId));
            return Ok(topicsWithTests);
        }

    }
}
