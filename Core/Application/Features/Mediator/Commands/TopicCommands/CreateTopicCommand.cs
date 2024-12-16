using MediatR;

namespace Application.Features.Mediator.Commands.TopicCommands
{
    public class CreateTopicCommand : IRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CourseID { get; set; }
    }
}
