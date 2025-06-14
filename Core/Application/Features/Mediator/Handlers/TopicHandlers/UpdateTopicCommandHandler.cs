using Application.Features.Mediator.Commands.TopicCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TopicHandlers
{
    public class UpdateTopicCommandHandler : IRequestHandler<UpdateTopicCommand>
    {
        private readonly IRepository<Topic> _repository;

        public UpdateTopicCommandHandler(IRepository<Topic> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await _repository.GetByIdAsync(request.TopicID);
            if (topic != null)
            {
                topic.Name = request.Name;
                topic.Description = request.Description;
                topic.VideoLink = request.VideoLink;
                topic.Order=request.Order;
                //topic.CourseID = request.CourseID;
                await _repository.UpdateAsync(topic);
            }
        }
    }
}
