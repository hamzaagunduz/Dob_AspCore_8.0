using Application.Features.Mediator.Commands.FlashCardCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    public class RemoveFlashCardCommandHandler : IRequestHandler<RemoveFlashCardCommand>
    {
        private readonly IRepository<FlashCard> _repository;

        public RemoveFlashCardCommandHandler(IRepository<FlashCard> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveFlashCardCommand request, CancellationToken cancellationToken)
        {
            var flashCard = await _repository.GetByIdAsync(request.FlashCardID);
            if (flashCard != null)
            {
                await _repository.RemoveAsync(flashCard);
            }
        }
    }
}
