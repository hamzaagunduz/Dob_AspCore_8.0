using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Interfaces.IFlashCardRepository;
using Application.Features.Mediator.Results.FlashCardResults;
using Application.Interfaces;
using Application.Features.Mediator.Queries.FlashCardQuery;

public class GetFlashCardsByTestIdQueryHandler : IRequestHandler<GetFlashCardsByTestIdQuery, List<GetFlashCardsByQuestionIdQueryResult>>
{
    private readonly IFlashCardRepository _flashCardRepository;

    public GetFlashCardsByTestIdQueryHandler(IFlashCardRepository flashCardRepository)
    {
        _flashCardRepository = flashCardRepository;
    }

    public async Task<List<GetFlashCardsByQuestionIdQueryResult>> Handle(GetFlashCardsByTestIdQuery request, CancellationToken cancellationToken)
    {

        var flashCard = await _flashCardRepository.GetFlashCardsByTestIdAsync(request.TestID);
        if (flashCard != null)
        {
            return flashCard.Select(flashCard => new GetFlashCardsByQuestionIdQueryResult
            {
                FlashCardID = flashCard.FlashCardID,
                Front = flashCard.Front,
                Back = flashCard.Back,
                QuestionID = flashCard.QuestionID
            }).ToList();
        }
        return null;
    }
}
