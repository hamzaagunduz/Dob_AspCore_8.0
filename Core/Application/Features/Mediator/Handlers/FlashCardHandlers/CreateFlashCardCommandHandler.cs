using Application.Features.Mediator.Commands.FlashCardCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    public class CreateFlashCardCommandHandler : IRequestHandler<CreateFlashCardCommand>
    {
        private readonly IRepository<FlashCard> _repository;

        public CreateFlashCardCommandHandler(IRepository<FlashCard> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateFlashCardCommand request, CancellationToken cancellationToken)
        {
            var flashCard = new FlashCard
            {
                Front = request.Front,
                Back = request.Back,
                QuestionID = request.QuestionID
            };

            await _repository.CreateAsync(flashCard);
        }
    }
}
