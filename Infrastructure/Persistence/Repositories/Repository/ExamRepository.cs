using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Repository
{
    using Application.Interfaces.IExamRepository;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Context;
    using System.Linq;
    using System.Threading.Tasks;

    namespace Infrastructure.Persistence.Repositories
    {
        public class ExamRepository : IExamRepository
        {
            private readonly DobContext _context;

            public ExamRepository(DobContext context)
            {
                _context = context;
            }

            public async Task<Exam> GetByIdAsync(int id)
            {
                return await _context.Set<Exam>().FindAsync(id);
            }

            public async Task DeselectAllExams()
            {
                var exams = await _context.Set<Exam>()
                                           .Where(e => e.Selected == true)
                                           .ToListAsync();
                foreach (var exam in exams)
                {
                    exam.Selected = false;
                }
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Exam exam)
            {
                _context.Set<Exam>().Update(exam);
                await _context.SaveChangesAsync();
            }
        }
    }

}
