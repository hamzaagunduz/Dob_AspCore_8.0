using Application.Features.Mediator.Queries.TopicQueries;
using Application.Features.Mediator.Results.TopicResults;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

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

        var orderedTopics = topics
            .OrderBy(topic => topic.Order ?? 0) // ✅ Order'e göre sırala, null'ları 0 say
            .Select(topic => new GetAllTopicQueryResult
            {
                TopicID = topic.TopicID,
                Name = topic.Name,
                Description = topic.Description,
                VideoLink = topic.VideoLink,
                CourseID = topic.CourseID
            })
            .ToList();

        return orderedTopics;
    }
}
