// Application/Fetaures/Mediator/Commands/QuestionCommands/UpdateFullQuestionCommand.cs
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Mediator.Commands.QuestionCommands
{
    public class UpdateFullQuestionCommand : IRequest
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public int TestId { get; set; }
        public int Answer { get; set; }

        public string? FlashCardFront { get; set; }
        public string? FlashCardBack { get; set; }

        public IFormFile? QuestionImage { get; set; }
        public IFormFile? OptionAImage { get; set; }
        public IFormFile? OptionBImage { get; set; }
        public IFormFile? OptionCImage { get; set; }
        public IFormFile? OptionDImage { get; set; }
        public IFormFile? OptionEImage { get; set; }

        public bool RemoveFlashCard { get; set; } // Added for explicit flashcard removal
    }
}