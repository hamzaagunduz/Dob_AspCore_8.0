using Application.Features.Mediator.Commands.TopicCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TopicHandlers
{
    public class RemoveTopicCommandHandler : IRequestHandler<RemoveTopicCommand>
    {
        private readonly IRepository<Topic> _repository;

        public RemoveTopicCommandHandler(IRepository<Topic> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await _repository.GetByIdAsync(request.TopicID);
            if (topic != null)
            {
                await _repository.RemoveAsync(topic);
            }
        }
    }
}
