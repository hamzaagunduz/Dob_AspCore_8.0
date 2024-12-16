using MediatR;

namespace Application.Features.Mediator.Commands.FlashCardCommands
{
    public class CreateFlashCardCommand : IRequest
    {
        public string Front { get; set; }
        public string Back { get; set; }
        public int QuestionID { get; set; }
    }
}
