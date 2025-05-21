using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Interfaces.IFlashCardRepository;
using Application.Features.Mediator.Results.FlashCardResults;
using Application.Interfaces;
using Application.Features.Mediator.Queries.FlashCardQuery;

public class GetFlashCardsByTestIdQueryHandler : IRequestHandler<GetFlashCardsByTestIdQuery, List<GetFlashCardsByTestIdQueryResult>>
{
    private readonly IFlashCardRepository _flashCardRepository;

    public GetFlashCardsByTestIdQueryHandler(IFlashCardRepository flashCardRepository)
    {
        _flashCardRepository = flashCardRepository;
    }

    public async Task<List<GetFlashCardsByTestIdQueryResult>> Handle(GetFlashCardsByTestIdQuery request, CancellationToken cancellationToken)
    {

        var flashCards = await _flashCardRepository.GetFlashCardsByTestIdAsync(request.TestID);
        var userId = request.UserID; 

        if (flashCards != null)
        {
            return flashCards.Select(flashCard => new GetFlashCardsByTestIdQueryResult
            {
                FlashCardID = flashCard.FlashCardID,
                Front = flashCard.Front,
                Back = flashCard.Back,
                QuestionID = flashCard.QuestionID,
                isFavoried = flashCard.AppUserFlashCards.Any(f => f.AppUserID == userId)
            }).ToList();
        }

        return null;

    }
}
