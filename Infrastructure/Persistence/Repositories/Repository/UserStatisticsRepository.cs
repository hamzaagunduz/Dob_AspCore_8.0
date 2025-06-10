using Application.Interfaces.IUserStatisticsRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Repository
{
    public class UserStatisticsRepository : IUserStatisticsRepository
    {
        private readonly DobContext _context;

        public UserStatisticsRepository(DobContext context)
        {
            _context = context;
        }

        public async Task<UserStatistics> GetByUserIdAsync(int userId)
        {
            return await _context.UserStatistics.FirstOrDefaultAsync(x => x.AppUserId == userId);
        }

        public async Task AddAsync(UserStatistics stats)
        {
            await _context.UserStatistics.AddAsync(stats);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserStatistics stats)
        {
            _context.UserStatistics.Update(stats);
            await _context.SaveChangesAsync();
        }

        public async Task<(AppUser user, UserStatistics statistics)> GetUserAndStatisticsAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var statistics = await _context.UserStatistics.FirstOrDefaultAsync(x => x.AppUserId == userId);

            return (user, statistics ?? new UserStatistics()); // Eğer kayıt yoksa boş nesne dön
        }

        public async Task<IEnumerable<UserStatistics>> GetByUserIdsAsync(IEnumerable<int> userIds)
        {
            return await _context.UserStatistics
                .Where(s => userIds.Contains(s.AppUserId))
                .ToListAsync();
        }
    }
}
