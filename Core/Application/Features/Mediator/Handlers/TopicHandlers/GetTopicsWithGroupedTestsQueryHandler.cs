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
    public class GetTopicsWithGroupedTestsQueryHandler : IRequestHandler<GetTopicsWithGroupedTestsQuery, List<GetTopicsWithGroupedTestsQueryResult>>
    {
        private readonly ITopicRepository _topicRepository;

        public GetTopicsWithGroupedTestsQueryHandler(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public async Task<List<GetTopicsWithGroupedTestsQueryResult>> Handle(GetTopicsWithGroupedTestsQuery request, CancellationToken cancellationToken)
        {
            var topics = await _topicRepository.GetTopicsWithTestsByCourseIdAsync(request.CourseId);

            return topics.Select(topic => new GetTopicsWithGroupedTestsQueryResult
            {
                TopicID = topic.TopicID,
                Name = topic.Name,
                Description = topic.Description,
                VideoLink = topic.VideoLink,
                TestGroups = topic.TestGroups.Select(tg => new TestGroupDto
                {
                    TestGroupID = tg.TestGroupID,
                    Title = tg.Title,
                    Description = tg.Description,
                    Tests = tg.Tests.Select(test => new TestDto
                    {
                        TestID = test.TestID,
                        Title = test.Title,
                        Description = test.Description
                    }).ToList()
                }).ToList()
            }).ToList();
        }
    }

}
