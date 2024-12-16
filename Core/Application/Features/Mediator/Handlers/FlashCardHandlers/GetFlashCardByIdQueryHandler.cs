using Application.Interfaces;
using Application.Features.Mediator.Results.FlashCardResults;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.FlashCardQueries;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    public class GetFlashCardByIdQueryHandler : IRequestHandler<GetFlashCardByIdQuery, GetFlashCardByIdQueryResult>
    {
        private readonly IRepository<FlashCard> _repository;

        public GetFlashCardByIdQueryHandler(IRepository<FlashCard> repository)
        {
            _repository = repository;
        }

        public async Task<GetFlashCardByIdQueryResult> Handle(GetFlashCardByIdQuery request, CancellationToken cancellationToken)
        {
            var flashCard = await _repository.GetByIdAsync(request.FlashCardID);
            if (flashCard != null)
            {
                return new GetFlashCardByIdQueryResult
                {
                    FlashCardID = flashCard.FlashCardID,
                    Front = flashCard.Front,
                    Back = flashCard.Back,
                    QuestionID = flashCard.QuestionID
                };
            }
            return null;
        }
    }
}
