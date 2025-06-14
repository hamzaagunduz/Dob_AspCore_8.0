using Application.Interfaces.IAppUserRepository;
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
    public class AppUserRepository : IAppUserRepository
    {
        private readonly DobContext _context;

        public AppUserRepository(DobContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetTotalDiamondsAsync()
        {
            return await _context.Users.SumAsync(u => u.Diamond ?? 0);
        }

        public async Task<List<AppUserInfoDto>> GetLastFiveUsersAsync()
        {
            return await _context.Users
                .OrderByDescending(u => u.Id) // Kayıt tarihi alanın varsa
                .Take(5)
                .Select(u => new AppUserInfoDto
                {
                    FirstName = u.FirstName,
                    SurName = u.SurName,
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<(List<AppUser> Users, int TotalCount)> GetPagedUsersWithStatisticsAsync(
            int pageNumber,
            int pageSize)
        {
            var query = _context.Users.AsQueryable();

            int totalCount = await query.CountAsync();

            var users = await query
                .OrderBy(u => u.Id) // ID'ye göre sırala, istediğiniz alanı kullanabilirsiniz
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }
    }
}
