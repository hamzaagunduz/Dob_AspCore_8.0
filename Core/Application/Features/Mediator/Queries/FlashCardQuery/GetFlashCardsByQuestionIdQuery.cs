using Application.Features.Mediator.Results.FlashCardResults;
using MediatR;
using System.Collections.Generic;

public class GetFlashCardsByQuestionIdQuery : IRequest<List<GetFlashCardsByQuestionIdQueryResult>>
{
    public int QuestionID { get; set; }

    public GetFlashCardsByQuestionIdQuery(int questionId)
    {
        QuestionID = questionId;
    }
}
