using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IUserLoginHistoryRepository
{
    public interface IUserLoginHistoryRepository
    {
        Task<int> GetDailyActiveUsersCountAsync(DateTime date);
        Task<Dictionary<DateTime, int>> GetWeeklyActiveUsersAsync();

        // Diğer metodlar...
    }
}
