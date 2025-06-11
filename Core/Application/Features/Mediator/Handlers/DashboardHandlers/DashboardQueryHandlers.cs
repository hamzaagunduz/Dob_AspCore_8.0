using Application.Features.Mediator.Queries.DashboardQuery;
using Application.Features.Mediator.Results.DashboardQueryResult;
using Application.Interfaces.IAppUserRepository;
using Application.Interfaces.ISystemMetricsService.cs;
using Application.Interfaces.IUserLoginHistoryRepository;
using Application.Interfaces.IUserStatisticsRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.DashboardHandlers
{
    public class DashboardQueryHandler : IRequestHandler<DashboardQuery, DashboardQueryResult>
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUserLoginHistoryRepository _loginHistoryRepository;
        private readonly IUserStatisticsRepository _statisticsRepository;
        private readonly ISystemMetricsService _systemMetricsService;

        public DashboardQueryHandler(
            IAppUserRepository appUserRepository,
            IUserLoginHistoryRepository loginHistoryRepository,
            IUserStatisticsRepository statisticsRepository,
            ISystemMetricsService systemMetricsService)
        {
            _appUserRepository = appUserRepository;
            _loginHistoryRepository = loginHistoryRepository;
            _statisticsRepository = statisticsRepository;
            _systemMetricsService = systemMetricsService;
        }

        public async Task<DashboardQueryResult> Handle(DashboardQuery request, CancellationToken cancellationToken)
        {
            var weeklyData = await _loginHistoryRepository.GetWeeklyActiveUsersAsync();
            var systemMetrics = await _systemMetricsService.GetSystemMetricsAsync();
            var lastFiveUsers = await _appUserRepository.GetLastFiveUsersAsync(); // ⬅️ BURASI

            return new DashboardQueryResult
            {
                TotalUsers = await _appUserRepository.GetTotalUserCountAsync(),
                DailyActiveUsers = await _loginHistoryRepository.GetDailyActiveUsersCountAsync(DateTime.UtcNow),
                AverageTestCompletion = await _statisticsRepository.GetAverageTestCompletionAsync(),
                TotalDiamonds = await _appUserRepository.GetTotalDiamondsAsync(),


                SystemInfo = new SystemInfo
                {
                    CpuUsage = systemMetrics.CpuUsage,
                    MemoryUsage = systemMetrics.MemoryUsage,
                    DiskUsage = systemMetrics.DiskUsage,
                    NetworkUsage = systemMetrics.NetworkUsage
                },

                WeeklyActiveUsers = weeklyData.Select(kvp => new WeeklyActiveUserData
                {
                    DayName = kvp.Key.ToString("dddd"),
                    ShortDayName = GetShortDayName(kvp.Key),
                    Count = kvp.Value
                }).ToList(),

                LastFiveUsers = lastFiveUsers.Select(u => new LastFiveUserDto
                {
                    FirstName = u.FirstName,
                    SurName = u.SurName,
                    Email = u.Email
                }).ToList()


            };
        }

        private string GetShortDayName(DateTime date)
        {
            var culture = new CultureInfo("tr-TR");
            return culture.DateTimeFormat.GetShortestDayName(date.DayOfWeek);
        }
    }
}
