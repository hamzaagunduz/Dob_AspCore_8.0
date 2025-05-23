﻿using Microsoft.EntityFrameworkCore;
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

    public async Task AddAsync(AppUserFlashCard entity)
    {
        _context.Set<AppUserFlashCard>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<AppUserFlashCard?> GetUserFlashCardAsync(int appUserId, int flashCardId)
    {
        return await _context.Set<AppUserFlashCard>()
            .FirstOrDefaultAsync(x => x.AppUserID == appUserId && x.FlashCardID == flashCardId);
    }

    public async Task RemoveUserFlashCardAsync(AppUserFlashCard entity)
    {
        _context.Set<AppUserFlashCard>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<FlashCard>> GetUserFavoriteFlashCardsByCourseAsync(int appUserId, int courseId)
    {
        return await _context.FlashCards
              .Where(f => f.AppUserFlashCards.Any(aufc => aufc.AppUserID == appUserId)
                          && f.Question.Test.Topic.CourseID == courseId)
              .ToListAsync();
    }

public async Task<List<FlashCard>> GetFlashCardsByTestIdAsync(int testId)
{
        return await _context.FlashCards
            .Include(fc => fc.Question)
            .Include(fc => fc.AppUserFlashCards) // favori bilgileri için gerekli
            .Where(fc => fc.Question.TestID == testId)
            .ToListAsync();
}


}
