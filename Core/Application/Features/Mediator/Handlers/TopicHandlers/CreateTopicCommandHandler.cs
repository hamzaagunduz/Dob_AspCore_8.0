using Application.Features.Mediator.Commands.TopicCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TopicHandlers
{
    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand>
    {
        private readonly IRepository<Topic> _repository;

        public CreateTopicCommandHandler(IRepository<Topic> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = new Topic
            {
                Name = request.Name,
                Description = request.Description,
                CourseID = request.CourseID
            };

            await _repository.CreateAsync(topic);
        }
    }
}
