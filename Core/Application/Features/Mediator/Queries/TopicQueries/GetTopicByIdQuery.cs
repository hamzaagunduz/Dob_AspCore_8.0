using Application.Features.Mediator.Results.TopicResults;
using MediatR;

namespace Application.Features.Mediator.Queries.TopicQueries
{
    public class GetTopicByIdQuery : IRequest<GetTopicByIdQueryResult>
    {
        public int TopicID { get; set; }

        public GetTopicByIdQuery(int topicID)
        {
            TopicID = topicID;
        }
    }
}
