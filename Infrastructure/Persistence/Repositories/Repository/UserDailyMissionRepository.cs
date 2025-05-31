using Application.Interfaces.IUserDailyMissionRepository;
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
    public class UserDailyMissionRepository : IUserDailyMissionRepository
    {
        private readonly DobContext _context;

        public UserDailyMissionRepository(DobContext context)
        {
            _context = context;
        }

        public async Task<List<UserDailyMission>> GetUserDailyMissionsByUserId(int userId)
        {
            return await _context.UserDailyMissions
                .Include(x => x.DailyMission)
                .Where(x => x.AppUserId == userId)
                .ToListAsync();
        }
        public async Task<List<(int DailyMissionId, string Title, string Description, int TargetValue, int CurrentValue, bool IsCompleted, DateTime? Date)>> GetUserDailyMissionResultsByUserId(int userId)
        {
            return await _context.UserDailyMissions
                .Include(x => x.DailyMission)
                .Where(x => x.AppUserId == userId)
                .Select(x => new ValueTuple<int, string, string, int, int, bool, DateTime?>(
                    x.DailyMissionId,
                    x.DailyMission.Title,
                    x.DailyMission.Description,
                    x.DailyMission.TargetValue,
                    x.CurrentValue,
                    x.IsCompleted,
                    x.Date
                ))
                .ToListAsync();
        }

        public async Task UpdateAsync(UserDailyMission mission)
        {
            _context.UserDailyMissions.Update(mission);
            await _context.SaveChangesAsync();
        }
        public async Task ResetAllUserDailyMissionsAsync()
        {
            var allMissions = await _context.UserDailyMissions.ToListAsync();

            foreach (var mission in allMissions)
            {
                mission.CurrentValue = 0;
                mission.IsCompleted = false;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<DailyMission>> GetAllDailyMissionsAsync()
        {
            return await _context.DailyMissions.ToListAsync();
        }


        public async Task CreateUserDailyMissionsForUserAsync(int appUserId)
        {
            // 1. Tüm DailyMissionları çek
            var allDailyMissions = await _context.DailyMissions.ToListAsync();

            // 2. Kullanıcının mevcut UserDailyMission kayıtlarını çek (varsa)
            var existingUserMissions = await _context.UserDailyMissions
                .Where(x => x.AppUserId == appUserId)
                .Select(x => x.DailyMissionId)
                .ToListAsync();

            // 3. Yeni eklenmesi gereken görevleri belirle (var olmayanlar)
            var missionsToAdd = allDailyMissions
                .Where(dm => !existingUserMissions.Contains(dm.Id))
                .Select(dm => new UserDailyMission
                {
                    AppUserId = appUserId,
                    DailyMissionId = dm.Id,
                    CurrentValue = 0,
                    IsCompleted = false,
                    Date = DateTime.UtcNow,
                })
                .ToList();

            // 4. Yeni görevler varsa topluca ekle
            if (missionsToAdd.Any())
            {
                await _context.UserDailyMissions.AddRangeAsync(missionsToAdd);
                await _context.SaveChangesAsync();
            }
        }
    }

}
