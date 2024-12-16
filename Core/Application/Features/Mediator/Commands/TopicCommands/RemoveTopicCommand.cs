using MediatR;

namespace Application.Features.Mediator.Commands.TopicCommands
{
    public class RemoveTopicCommand : IRequest
    {
        public int TopicID { get; set; }

        public RemoveTopicCommand(int topicID)
        {
            TopicID = topicID;
        }
    }
}
