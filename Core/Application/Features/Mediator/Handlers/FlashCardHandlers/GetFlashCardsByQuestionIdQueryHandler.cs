using Application.Features.Mediator.Results.FlashCardResults;
using Application.Interfaces.IFlashCardRepository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GetFlashCardsByQuestionIdQueryHandler : IRequestHandler<GetFlashCardsByQuestionIdQuery, List<GetFlashCardsByQuestionIdQueryResult>>
{
    private readonly IFlashCardRepository _flashCardRepository;

    public GetFlashCardsByQuestionIdQueryHandler(IFlashCardRepository flashCardRepository)
    {
        _flashCardRepository = flashCardRepository;
    }

    public async Task<List<GetFlashCardsByQuestionIdQueryResult>> Handle(GetFlashCardsByQuestionIdQuery request, CancellationToken cancellationToken)
    {
        var flashCards = await _flashCardRepository.GetFlashCardsByQuestionIdAsync(request.QuestionID);

        // Data transfer object dönüşümü
        return flashCards.Select(fc => new GetFlashCardsByQuestionIdQueryResult
        {
            FlashCardID = fc.FlashCardID,
            Front = fc.Front,
            Back = fc.Back,
            QuestionID = fc.QuestionID
        }).ToList();
    }
}
