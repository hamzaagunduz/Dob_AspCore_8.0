using Application.Interfaces.IQuestionRepository;
using Domain.Entities;
using Domain.Enums;
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
                .Include(q => q.Images) // <-- ilişkili resimleri yükler
                .Where(q => q.TestID == testId)
                .ToListAsync();
        }


        public async Task<QuestionImage?> GetByQuestionIdAndTypeAsync(int questionId, QuestionImageType type)
        {
            return await _context.QuestionImages
                .FirstOrDefaultAsync(qi => qi.QuestionID == questionId && qi.Type == type);
        }

        public async Task UpdateImgAsync(QuestionImage image)
        {
            _context.QuestionImages.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task AddImgAsync(QuestionImage image)
        {
            await _context.QuestionImages.AddAsync(image);
            await _context.SaveChangesAsync();
        }


    }
}
