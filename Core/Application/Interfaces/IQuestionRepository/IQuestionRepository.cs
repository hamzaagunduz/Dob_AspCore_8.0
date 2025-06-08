using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IQuestionRepository
{
    public interface IQuestionRepository
    {
        Task<List<Question>> GetQuestionsByTestIdAsync(int testId);
        Task AddImgAsync(QuestionImage image);

        Task<QuestionImage?> GetByQuestionIdAndTypeAsync(int questionId, QuestionImageType type);

        Task UpdateImgAsync(QuestionImage image);

    }

}
