using MediatR;
using System.Collections.Generic;
using Domain.Entities;
using Application.Features.Mediator.Results.FlashCardResults;

public class GetFlashCardsByTestIdQuery : IRequest<List<GetFlashCardsByTestIdQueryResult>>
{
    public int TestID { get; set; }
    public int UserID { get; set; }

    public GetFlashCardsByTestIdQuery(int testId, int userID)
    {
        TestID = testId;
        UserID = userID;
    }
}
