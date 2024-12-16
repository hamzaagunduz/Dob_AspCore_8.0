using Application.Features.Mediator.Results.QuestionResults;
using MediatR;

namespace Application.Features.Mediator.Queries.QuestionQueries
{
    public class GetQuestionByIdQuery : IRequest<GetQuestionByIdQueryResult>
    {
        public int QuestionID { get; set; }

        public GetQuestionByIdQuery(int questionID)
        {
            QuestionID = questionID;
        }
    }
}
