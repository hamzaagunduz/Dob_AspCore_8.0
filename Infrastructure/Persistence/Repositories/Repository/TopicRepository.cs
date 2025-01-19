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

        public async Task<List<Topic>> GetTopicsWithTestsByCourseIdAsync(int examId)
        {
            return await _context.Set<Topic>()
                .Include(t => t.Tests) // Test ilişkisini dahil ediyoruz.
                .Where(t => t.CourseID == examId) // ExamID'ye göre filtreleme.
                .ToListAsync();
        }
    }
}
