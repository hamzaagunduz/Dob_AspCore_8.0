using Application.Features.Mediator.Results.QuestionResults;
using MediatR;
using System.Collections.Generic;

public class GetQuestionsByTestIdQuery : IRequest<List<GetQuestionsByTestIdQueryResult>>
{
    public int TestID { get; set; }

    public GetQuestionsByTestIdQuery(int testId)
    {
        TestID = testId;
    }
}
