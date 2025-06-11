using Application.Interfaces.IUserLoginHistoryRepository;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Repository
{
    public class UserLoginHistoryRepository: IUserLoginHistoryRepository
    {
        private readonly DobContext _context;

        public UserLoginHistoryRepository(DobContext context)
        {
            _context = context;
        }

        public async Task<int> GetDailyActiveUsersCountAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);
            return await _context.UserLoginHistories
                .Where(ulh => ulh.LoginTime >= startDate && ulh.LoginTime < endDate)
                .Select(ulh => ulh.UserId)
                .Distinct()
                .CountAsync();
        }

        public async Task<Dictionary<DateTime, int>> GetWeeklyActiveUsersAsync()
        {
            var endDate = DateTime.UtcNow.Date;
            var startDate = endDate.AddDays(-6); // Son 7 gün (bugün dahil)

            var dailyCounts = new Dictionary<DateTime, int>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var count = await GetDailyActiveUsersCountAsync(date);
                dailyCounts.Add(date, count);
            }

            return dailyCounts;
        }
    }
}
