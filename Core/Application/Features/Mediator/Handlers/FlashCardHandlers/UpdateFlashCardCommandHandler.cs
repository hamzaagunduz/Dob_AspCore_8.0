using Application.Features.Mediator.Commands.FlashCardCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    public class UpdateFlashCardCommandHandler : IRequestHandler<UpdateFlashCardCommand>
    {
        private readonly IRepository<FlashCard> _repository;

        public UpdateFlashCardCommandHandler(IRepository<FlashCard> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateFlashCardCommand request, CancellationToken cancellationToken)
        {
            var flashCard = await _repository.GetByIdAsync(request.FlashCardID);
            if (flashCard != null)
            {
                flashCard.Front = request.Front;
                flashCard.Back = request.Back;
                flashCard.QuestionID = request.QuestionID;
                await _repository.UpdateAsync(flashCard);
            }
        }
    }
}
