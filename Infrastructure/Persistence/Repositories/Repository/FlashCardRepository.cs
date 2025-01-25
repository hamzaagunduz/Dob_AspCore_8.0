using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.IFlashCardRepository;

public class FlashCardRepository : IFlashCardRepository
{
    private readonly DobContext _context;

    public FlashCardRepository(DobContext context)
    {
        _context = context;
    }

    public async Task<List<FlashCard>> GetFlashCardsByQuestionIdAsync(int questionId)
    {
        return await _context.FlashCards
            .Where(fc => fc.QuestionID == questionId)
            .ToListAsync();
    }
}
