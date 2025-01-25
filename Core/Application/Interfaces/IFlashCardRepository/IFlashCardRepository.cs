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
    }
}
