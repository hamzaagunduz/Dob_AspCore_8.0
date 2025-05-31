using Application.Features.Mediator.Results.DailyMissionResult;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IUserDailyMissionRepository
{
    public interface IUserDailyMissionRepository
    {
        Task<List<UserDailyMission>> GetUserDailyMissionsByUserId(int userId);
        Task<List<(int DailyMissionId, string Title, string Description, int TargetValue, int CurrentValue, bool IsCompleted, DateTime? Date)>> GetUserDailyMissionResultsByUserId(int userId);

        Task UpdateAsync(UserDailyMission mission);

        Task ResetAllUserDailyMissionsAsync();
        Task<List<DailyMission>> GetAllDailyMissionsAsync();
        Task CreateUserDailyMissionsForUserAsync(int appUserId);

    }

}
