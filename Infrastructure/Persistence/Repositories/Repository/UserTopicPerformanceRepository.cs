using Application.Interfaces.IUserTopicPerformanceRepository;
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
    public class UserTopicPerformanceRepository : IUserTopicPerformanceRepository
    {
        private readonly DobContext _context;

        public UserTopicPerformanceRepository(DobContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<UserTopicPerformance> performances)
        {
            await _context.UserTopicPerformances.AddRangeAsync(performances);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<UserTopicPerformance> GetQueryable()
        {
            return _context.UserTopicPerformances
                .Include(p => p.Topic)
                    .ThenInclude(t => t.Course)
                .AsNoTracking();
        }
    }

}
