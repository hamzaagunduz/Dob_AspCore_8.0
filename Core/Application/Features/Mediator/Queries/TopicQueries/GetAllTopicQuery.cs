using Application.Features.Mediator.Results.TopicResults;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Mediator.Queries.TopicQueries
{
    public class GetAllTopicQuery : IRequest<List<GetAllTopicQueryResult>>
    {
    }
}
