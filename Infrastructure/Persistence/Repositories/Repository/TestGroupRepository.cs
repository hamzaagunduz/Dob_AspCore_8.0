using Application.Interfaces.ITestGroupRepository;
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
    public class TestGroupRepository : ITestGroupRepository
    {
        private readonly DobContext _context;

        public TestGroupRepository(DobContext context)
        {
            _context = context;
        }

        public async Task<TestGroup> AddAsync(TestGroup testGroup)
        {
            _context.TestGroups.Add(testGroup);
            await _context.SaveChangesAsync();
            return testGroup;
        }

        public async Task<Test?> GetTestWithGroupAndTopicAsync(int testId)
        {
            return await _context.Tests
                .Include(t => t.TestGroup)
                .ThenInclude(g => g.Topic)
                .FirstOrDefaultAsync(t => t.TestID == testId);
        }

        public async Task<int> GetMaxOrderByTestGroupIdAsync(int testGroupId)
        {
            var maxOrder = await _context.Tests
                .Where(t => t.TestGroupID == testGroupId && t.Order.HasValue)
                .MaxAsync(t => (int?)t.Order) ?? 0;

            return maxOrder;
        }

        public async Task<int> GetMaxOrderByTopicIdAsync(int? topicId)
        {
            var maxOrder = await _context.TestGroups
                .Where(tg => tg.TopicID == topicId && tg.Order.HasValue)
                .MaxAsync(tg => (int?)tg.Order) ?? 0;

            return maxOrder;
        }
    }
}
