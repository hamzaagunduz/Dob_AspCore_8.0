using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IUserStatisticsRepository
{
    public interface IUserStatisticsRepository
    {
        Task<UserStatistics> GetByUserIdAsync(int userId);
        Task AddAsync(UserStatistics stats);
        Task UpdateAsync(UserStatistics stats);
        Task<(AppUser user, UserStatistics statistics)> GetUserAndStatisticsAsync(int userId);
        Task<IEnumerable<UserStatistics>> GetByUserIdsAsync(IEnumerable<int> userIds); // New method

    }
}
