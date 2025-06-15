using Application.Features.Mediator.Queries.FlashCardQuery;
using Application.Features.Mediator.Results.FlashCardResults;
using Application.Interfaces.IFlashCardRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    public class GetUserCustomFlashCardsQueryHandler : IRequestHandler<GetUserCustomFlashCardsQuery, List<GetUserCustomFlashCardsQueryResult>>
    {
        private readonly IFlashCardRepository _repository;

        public GetUserCustomFlashCardsQueryHandler(IFlashCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetUserCustomFlashCardsQueryResult>> Handle(
            GetUserCustomFlashCardsQuery request,
            CancellationToken cancellationToken)
        {
            var cards = await _repository.GetUserCustomFlashCardsByUserAsync(request.AppUserId);

            return cards.Select(card => new GetUserCustomFlashCardsQueryResult
            {
                UserCustomFlashCardID = card.UserCustomFlashCardID,
                Front = card.Front,
                Back = card.Back,
                CourseID = card.CourseID,
                CreatedAt = card.CreatedAt
            }).ToList();
        }
    }
}
