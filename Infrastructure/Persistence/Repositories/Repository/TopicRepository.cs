using Application.Interfaces.ITopicRepository;
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
    public class TopicRepository : ITopicRepository
    {
        private readonly DobContext _context;

        public TopicRepository(DobContext context)
        {
            _context = context;
        }

        public async Task<List<Topic>> GetTopicsWithTestsByCourseIdAsync(int courseId)
        {
            return await _context.Set<Topic>()
                .Include(t => t.TestGroups)
                    .ThenInclude(tg => tg.Tests)
                .Where(t => t.CourseID == courseId)
                .ToListAsync();
        }

    }
}
