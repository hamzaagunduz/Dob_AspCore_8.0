using Application.Features.Mediator.Queries.FlashCardQuery;
using Application.Features.Mediator.Results.FlashCardResults;
using Application.Interfaces.IFlashCardRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    // GetQuizFromFavoritesQueryHandler.cs
    public class GetQuizFromFavoritesQueryHandler : IRequestHandler<GetQuizFromFavoritesQuery, GetQuizFromFavoritesQueryResult>
    {
        private readonly IFlashCardRepository _flashCardRepository;

        public GetQuizFromFavoritesQueryHandler(IFlashCardRepository flashCardRepository)
        {
            _flashCardRepository = flashCardRepository;
        }

        public async Task<GetQuizFromFavoritesQueryResult> Handle(
            GetQuizFromFavoritesQuery request,
            CancellationToken cancellationToken)
        {
            var result = new GetQuizFromFavoritesQueryResult();

            // 1. Kullanıcının favori kartlarını rastgele getir
            var favoriteCards = await _flashCardRepository.GetRandomFavoriteFlashCardsByUserAsync(
                request.AppUserId,
                request.CourseId,
                request.QuestionCount);

            // 2. Her favori kart için quiz sorusu oluştur
            foreach (var card in favoriteCards)
            {
                var question = new GetQuizFromFavoritesQueryResult.QuizItem
                {
                    FlashCardID = card.FlashCardID,
                    QuestionText = card.Front,
                    CorrectAnswerId = card.FlashCardID
                };

                // 3. Yanlış cevapları getir (rastgele 3 kart)
                var wrongAnswers = await _flashCardRepository.GetRandomFlashCardsByCourseAsync(
                    request.CourseId,
                    3,
                    card.FlashCardID);

                // 4. Doğru cevabı ekle (kartın kendisi)
                question.Options.Add(new GetQuizFromFavoritesQueryResult.AnswerOption
                {
                    FlashCardID = card.FlashCardID,
                    AnswerText = card.Back
                });

                // 5. Yanlış cevapları ekle
                question.Options.AddRange(wrongAnswers.Select(w =>
                    new GetQuizFromFavoritesQueryResult.AnswerOption
                    {
                        FlashCardID = w.FlashCardID,
                        AnswerText = w.Back
                    }));

                // 6. Seçenekleri karıştır
                question.Options = question.Options
                    .OrderBy(x => Guid.NewGuid())
                    .ToList();

                result.Questions.Add(question);
            }

            return result;
        }
    }
}
