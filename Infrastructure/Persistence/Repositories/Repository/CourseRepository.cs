using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.ICourseRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DobContext _context;

        public CourseRepository(DobContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Course>> GetCoursesByExamIdAsync(int examId)
        {
            return await _context.Set<Course>()
                .Where(c => c.ExamID == examId)
                .Include(c => c.Topics) // Eğer ilişkili Topic'leri de çekmek istiyorsanız
                .ToListAsync();
        }
    }
}
