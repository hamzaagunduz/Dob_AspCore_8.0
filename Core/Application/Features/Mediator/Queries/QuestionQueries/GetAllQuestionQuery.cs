using Application.Features.Mediator.Results.QuestionResults;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Mediator.Queries.QuestionQueries
{
    public class GetAllQuestionQuery : IRequest<List<GetAllQuestionQueryResult>>
    {
    }
}
