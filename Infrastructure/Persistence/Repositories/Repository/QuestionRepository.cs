using Application.Interfaces.IQuestionRepository;
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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DobContext _context;

        public QuestionRepository(DobContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetQuestionsByTestIdAsync(int testId)
        {
            return await _context.Questions
                .Where(q => q.TestID == testId)
                .ToListAsync();
        }

        public async Task AddImgAsync(QuestionImage image)
        {
            await _context.QuestionImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }
    }
}
