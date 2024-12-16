using Application.Interfaces;
using Application.Features.Mediator.Results.TopicResults;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.TopicQueries;

namespace Application.Features.Mediator.Handlers.TopicHandlers
{
    public class GetAllTopicQueryHandler : IRequestHandler<GetAllTopicQuery, List<GetAllTopicQueryResult>>
    {
        private readonly IRepository<Topic> _repository;

        public GetAllTopicQueryHandler(IRepository<Topic> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllTopicQueryResult>> Handle(GetAllTopicQuery request, CancellationToken cancellationToken)
        {
            var topics = await _repository.GetAllAsync();
            return topics.Select(topic => new GetAllTopicQueryResult
            {
                TopicID = topic.TopicID,
                Name = topic.Name,
                Description = topic.Description,
                CourseID = topic.CourseID
            }).ToList();
        }
    }
}
