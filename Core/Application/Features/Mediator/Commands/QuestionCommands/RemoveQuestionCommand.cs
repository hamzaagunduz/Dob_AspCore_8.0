using MediatR;

namespace Application.Features.Mediator.Commands.QuestionCommands
{
    public class RemoveQuestionCommand : IRequest
    {
        public int QuestionID { get; set; }

        public RemoveQuestionCommand(int questionID)
        {
            QuestionID = questionID;
        }
    }
}
