using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.FlashCardResults
{
    // GetQuizFromFavoritesQueryResult.cs
    public class GetQuizFromFavoritesQueryResult
    {
        public List<QuizItem> Questions { get; set; } = new();

        public class QuizItem
        {
            public int FlashCardID { get; set; }
            public string QuestionText { get; set; }  // Front bilgisi
            public int CorrectAnswerId { get; set; }   // Doğru cevap FlashCardID
            public List<AnswerOption> Options { get; set; } = new();
        }

        public class AnswerOption
        {
            public int FlashCardID { get; set; }
            public string AnswerText { get; set; }     // Back bilgisi
        }
    }
}
