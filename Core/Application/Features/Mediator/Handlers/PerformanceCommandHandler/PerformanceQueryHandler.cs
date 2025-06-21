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
            // Türkiye saat dilimi
            var turkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            var turkeyNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, turkeyTimeZone);

            DateTime fromDate = request.Range switch
            {
                "daily" => turkeyNow.Date,
                "weekly" => turkeyNow.Date.AddDays(-7),
                "monthly" => turkeyNow.Date.AddMonths(-1),
                _ => DateTime.MinValue
            };

            DateTime? toDate = null;
            if (request.Range == "daily")
            {
                toDate = fromDate.AddDays(1); // Ertesi gün
            }

            IQueryable<UserTopicPerformance> query = _performanceRepository
                .GetQueryable()
                .Where(p => p.AppUserId == request.userId && p.CompletedAt >= fromDate);

            if (toDate.HasValue)
            {
                query = query.Where(p => p.CompletedAt < toDate.Value);
            }

            var performances = await query.ToListAsync(cancellationToken);

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
