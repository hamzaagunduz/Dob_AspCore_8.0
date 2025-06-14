using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Features.Mediator.Results.AppUserResults;
using Application.Interfaces.IAppUserRepository;
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
    public class GetAppUsersWithPaginationQueryHandler :
         IRequestHandler<GetAppUsersWithPaginationQuery, GetAppUsersWithPaginationResult>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUserStatisticsRepository _userStatisticsRepository;

        public GetAppUsersWithPaginationQueryHandler(
            IAppUserRepository appUserRepository,
            IUserStatisticsRepository userStatisticsRepository)
        {
            _appUserRepository = appUserRepository;
            _userStatisticsRepository = userStatisticsRepository;
        }

        public async Task<GetAppUsersWithPaginationResult> Handle(
            GetAppUsersWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            // Sayfalama ile kullanıcıları ve toplam sayıyı al
            var (users, totalCount) = await _appUserRepository.GetPagedUsersWithStatisticsAsync(
                request.PageNumber,
                request.PageSize);

            var userIds = users.Select(u => u.Id).ToList();

            // İstatistikleri tek seferde getir
            var statisticsDict = (await _userStatisticsRepository.GetByUserIdsAsync(userIds))
                .ToDictionary(s => s.AppUserId);

            var userResults = users.Select(user =>
            {
                var stats = statisticsDict.GetValueOrDefault(user.Id) ?? new UserStatistics();

                return new GetAllAppUserQueryResult
                {
                    UserId = user.Id,
                    Email = user.Email,
                    SurName = user.SurName,
                    ExamID = user.ExamID,
                    FirstName = user.FirstName,
                    ImageURL = user.ImageURL,
                    Ban = user.Ban,
                    Diamond = user.Diamond,
                    LastLifeAddedTime = user.LastLifeAddedTime,

                    TotalScore = stats.TotalScore,
                    TotalTestsCompleted = stats.TotalTestsCompleted,
                    PerfectTestsCompleted = stats.PerfectTestsCompleted,
                    AverageScore = stats.Score,
                    League = stats.League,
                    ConsecutiveDays = stats.ConsecutiveDays,
                    ConsecutiveDaysTemp = stats.ConsecutiveDaysTemp,
                    LastTestDate = stats.LastTestDate
                };
            }).ToList();

            // Sayfa hesaplamaları
            int totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            return new GetAppUsersWithPaginationResult
            {
                Users = userResults,
                CurrentPage = request.PageNumber,
                TotalPages = totalPages,
                TotalCount = totalCount
            };
        }
    }
}
