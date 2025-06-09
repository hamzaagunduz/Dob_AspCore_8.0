using MediatR;

namespace Application.Features.Mediator.Commands.TopicCommands
{
    public class UpdateTopicCommand : IRequest
    {
        public int TopicID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }

        //public int CourseID { get; set; }
    }
}
