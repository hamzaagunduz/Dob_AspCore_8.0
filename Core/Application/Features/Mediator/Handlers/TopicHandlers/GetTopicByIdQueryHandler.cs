using Application.Interfaces;
using Application.Features.Mediator.Results.TopicResults;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.TopicQueries;

namespace Application.Features.Mediator.Handlers.TopicHandlers
{
    public class GetTopicByIdQueryHandler : IRequestHandler<GetTopicByIdQuery, GetTopicByIdQueryResult>
    {
        private readonly IRepository<Topic> _repository;

        public GetTopicByIdQueryHandler(IRepository<Topic> repository)
        {
            _repository = repository;
        }

        public async Task<GetTopicByIdQueryResult> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
        {
            var topic = await _repository.GetByIdAsync(request.TopicID);
            if (topic != null)
            {
                return new GetTopicByIdQueryResult
                {
                    TopicID = topic.TopicID,
                    Name = topic.Name,
                    Description = topic.Description,
                    CourseID = topic.CourseID
                };
            }
            return null;
        }
    }
}
