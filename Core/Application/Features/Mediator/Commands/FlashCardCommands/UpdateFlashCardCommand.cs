using MediatR;

namespace Application.Features.Mediator.Commands.FlashCardCommands
{
    public class UpdateFlashCardCommand : IRequest
    {
        public int FlashCardID { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int QuestionID { get; set; }
    }
}
