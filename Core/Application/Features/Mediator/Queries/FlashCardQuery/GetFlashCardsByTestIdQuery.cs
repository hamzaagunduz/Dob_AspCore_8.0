using MediatR;
using System.Collections.Generic;
using Domain.Entities;
using Application.Features.Mediator.Results.FlashCardResults;

public class GetFlashCardsByTestIdQuery : IRequest<List<GetFlashCardsByQuestionIdQueryResult>>
{
    public int TestID { get; set; }

    public GetFlashCardsByTestIdQuery(int testId)
    {
        TestID = testId;
    }
}
