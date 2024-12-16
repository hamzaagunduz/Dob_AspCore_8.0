using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.FlashCardQuery;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    public class GetAllFlashCardQueryHandler : IRequestHandler<GetAllFlashCardQuery, List<GetAllFlashCardQueryResult>>
    {
        private readonly IRepository<FlashCard> _repository;

        public GetAllFlashCardQueryHandler(IRepository<FlashCard> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllFlashCardQueryResult>> Handle(GetAllFlashCardQuery request, CancellationToken cancellationToken)
        {
            var flashCards = await _repository.GetAllAsync();
            return flashCards.Select(flashCard => new GetAllFlashCardQueryResult
            {
                FlashCardID = flashCard.FlashCardID,
                Front = flashCard.Front,
                Back = flashCard.Back,
                QuestionID = flashCard.QuestionID
            }).ToList();
        }
    }
}
