using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Features.Mediator.Results.AppUserResults;
using Application.Interfaces;
using Application.Interfaces.IUserStatisticsRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetAllAppUserQueryHandler : IRequestHandler<GetAllAppUserQuery, List<GetAllAppUserQueryResult>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IUserStatisticsRepository _userStatisticsRepository;

        public GetAllAppUserQueryHandler(
            IRepository<AppUser> repository,
            IUserStatisticsRepository userStatisticsRepository)
        {
            _repository = repository;
            _userStatisticsRepository = userStatisticsRepository;
        }

        public async Task<List<GetAllAppUserQueryResult>> Handle(
            GetAllAppUserQuery request,
            CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();
            var userIds = users.Select(u => u.Id).ToList();

            // Fetch all statistics in a single query
            var statisticsDict = (await _userStatisticsRepository.GetByUserIdsAsync(userIds))
                .ToDictionary(s => s.AppUserId);

            return users.Select(user =>
            {
                var stats = statisticsDict.GetValueOrDefault(user.Id) ?? new UserStatistics();

                return new GetAllAppUserQueryResult
                {
                    // Existing user properties
                    UserId = user.Id,
                    Email = user.Email,
                    SurName = user.SurName,
                    ExamID = user.ExamID,
                    FirstName = user.FirstName,
                    ImageURL = user.ImageURL,
                    Ban = user.Ban,
                    Diamond = user.Diamond,
                    LastLifeAddedTime = user.LastLifeAddedTime,

                    // Statistics properties
                    TotalScore = stats.TotalScore,
                    TotalTestsCompleted = stats.TotalTestsCompleted,
                    PerfectTestsCompleted = stats.PerfectTestsCompleted,
                    AverageScore = stats.Score, // Mapped to renamed property
                    League = stats.League,
                    ConsecutiveDays = stats.ConsecutiveDays,
                    ConsecutiveDaysTemp = stats.ConsecutiveDaysTemp,
                    LastTestDate = stats.LastTestDate
                };
            }).ToList();
        }
    }
}
