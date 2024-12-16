using MediatR;

namespace Application.Features.Mediator.Commands.FlashCardCommands
{
    public class RemoveFlashCardCommand : IRequest
    {
        public int FlashCardID { get; set; }

        public RemoveFlashCardCommand(int flashCardID)
        {
            FlashCardID = flashCardID;
        }
    }
}
