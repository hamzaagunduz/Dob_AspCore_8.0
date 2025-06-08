using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IFlashCardRepository
{
    public interface IFlashCardRepository
    {
        Task<List<FlashCard>> GetFlashCardsByQuestionIdAsync(int questionId);
        Task<FlashCard> GetByIdAsync(int id);
        Task CreateAsync(FlashCard entity);
        Task UpdateAsync(FlashCard entity);
        Task DeleteAsync(FlashCard entity);
        Task AddAsync(AppUserFlashCard entity);
        Task RemoveUserFlashCardAsync(AppUserFlashCard entity);
        Task<AppUserFlashCard?> GetUserFlashCardAsync(int appUserId, int flashCardId);
        Task<List<FlashCard>> GetUserFavoriteFlashCardsByCourseAsync(int appUserId, int courseId);

        Task<List<FlashCard>> GetFlashCardsByTestIdAsync(int testId);



    }
}
