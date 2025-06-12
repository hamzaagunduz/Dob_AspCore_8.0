using Application.Features.Mediator.Queries.TestQueries;
using Application.Features.Mediator.Results.TestResults;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class GetTestWithQuestionsQueryHandler : IRequestHandler<GetTestWithQuestionsQuery, GetTestWithQuestionsQueryResult>
    {
        private readonly IRepository<Test> _repository;
        private readonly IRepository<AppUserFlashCard> _appUserFlashCardRepository;

        public GetTestWithQuestionsQueryHandler(IRepository<Test> repository, IRepository<AppUserFlashCard> appUserFlashCardRepository)
        {
            _repository = repository;
            _appUserFlashCardRepository = appUserFlashCardRepository;
        }

        public async Task<GetTestWithQuestionsQueryResult> Handle(GetTestWithQuestionsQuery request, CancellationToken cancellationToken)
        {
            var test = await _repository.Table
                .Include(t => t.TestGroup)
                    .ThenInclude(g => g.Topic)
                .Include(t => t.Questions)
                    .ThenInclude(q => q.FlashCard)
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Images)
                .FirstOrDefaultAsync(t => t.TestID == request.TestID, cancellationToken);

            if (test == null) return null;

            // Kullanıcının yıldızladığı flashcardların ID'lerini çek
            var starredFlashCardIds = await _appUserFlashCardRepository.Table
                .Where(ufc => ufc.AppUserID == request.AppUserID)
                .Select(ufc => ufc.FlashCardID)
                .ToListAsync(cancellationToken);

            return new GetTestWithQuestionsQueryResult
            {
                TestID = test.TestID,
                Title = test.Title,
                Description = test.Description,
                TopicID = test.TestGroup?.TopicID,
                Questions = test.Questions.Select(q => new QuestionWithFlashCardResult
                {
                    QuestionID = q.QuestionID,
                    Text = q.Text,
                    Answer = q.Answer,
                    OptionA = q.OptionA,
                    OptionB = q.OptionB,
                    OptionC = q.OptionC,
                    OptionD = q.OptionD,
                    OptionE = q.OptionE,
                    FlashCard = q.FlashCard != null ? new FlashCardDto
                    {
                        FlashCardID = q.FlashCard.FlashCardID,
                        Front = q.FlashCard.Front,
                        Back = q.FlashCard.Back
                    } : null,
                    Images = q.Images?.Select(img => new QuestionImageDto
                    {
                        ImageUrl = img.ImageUrl,
                        Type = img.Type.HasValue ? (int)img.Type.Value : (int?)null
                    }).ToList() ?? new List<QuestionImageDto>()

                }).ToList(),

                StarredFlashCardIds = starredFlashCardIds
            };
        }

    }
}
