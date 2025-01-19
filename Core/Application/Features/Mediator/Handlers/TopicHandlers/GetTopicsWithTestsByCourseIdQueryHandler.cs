using Application.Features.Mediator.Queries.TopicQueries;
using Application.Features.Mediator.Results.TopicResults;
using Application.Interfaces.ITopicRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TopicHandlers
{
    public class GetTopicsWithTestsByCourseIdQueryHandler : IRequestHandler<GetTopicsWithTestsByCourseIdQuery, List<GetTopicsWithTestsByCourseIdQueryQueryResult>>
    {
        private readonly ITopicRepository _topicRepository;

        public GetTopicsWithTestsByCourseIdQueryHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<List<GetTopicsWithTestsByCourseIdQueryQueryResult>> Handle(GetTopicsWithTestsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            var topics = await _topicRepository.GetTopicsWithTestsByCourseIdAsync(request.CourseId);

            return topics.Select(topic => new GetTopicsWithTestsByCourseIdQueryQueryResult
            {
                TopicID = topic.TopicID,
                Name = topic.Name,
                Description = topic.Description,
                Tests = topic.Tests.Select(test => new TestDto
                {
                    TestID = test.TestID,
                    Title = test.Title,
                    Description = test.Description
                }).ToList()
            }).ToList();
        }
    }
}
