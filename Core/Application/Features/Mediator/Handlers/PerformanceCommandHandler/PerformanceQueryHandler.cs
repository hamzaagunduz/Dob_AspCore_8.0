using Application.Features.Mediator.Queries.PerformanceQuery;
using Application.Features.Mediator.Results.PerformanceQueryResult;
using Application.Interfaces.IUserTopicPerformanceRepository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.PerformanceCommandHandler
{
    public class PerformanceQueryHandler : IRequestHandler<PerformanceQuery, PerformanceQueryResult>
    {
        private readonly IUserTopicPerformanceRepository _performanceRepository;

        public PerformanceQueryHandler(IUserTopicPerformanceRepository performanceRepository)
        {
            _performanceRepository = performanceRepository;
        }

        public async Task<PerformanceQueryResult> Handle(PerformanceQuery request, CancellationToken cancellationToken)
        {
            DateTime fromDate = request.Range switch
            {
                "daily" => DateTime.Today,
                "weekly" => DateTime.Now.AddDays(-7),
                "monthly" => DateTime.Now.AddMonths(-1),
                _ => DateTime.MinValue
            };

            IQueryable<UserTopicPerformance> query = _performanceRepository
                .GetQueryable()
                .Where(p => p.AppUserId == request.userId && p.CompletedAt >= fromDate);

            if (request.Range == "daily")
            {
                var tomorrow = DateTime.Today.AddDays(1);
                query = query.Where(p => p.CompletedAt < tomorrow);
            }

            var performances = await query
                .ToListAsync(cancellationToken);

            var result = new PerformanceQueryResult
            {
                Courses = performances
                    .GroupBy(p => p.Topic.Course.Name)
                    .ToDictionary(
                        courseGroup => courseGroup.Key,
                        courseGroup => courseGroup
                            .GroupBy(x => x.Topic.Name)
                            .Select(topicGroup => new TopicPerformanceDto
                            {
                                Name = topicGroup.Key,
                                Correct = topicGroup.Sum(x => x.CorrectCount),
                                Wrong = topicGroup.Sum(x => x.WrongCount),
                                Time = topicGroup.Sum(x => x.DurationInMinutes)
                            })
                            .ToList()
                    )
            };

            return result;
        }
    }
}
