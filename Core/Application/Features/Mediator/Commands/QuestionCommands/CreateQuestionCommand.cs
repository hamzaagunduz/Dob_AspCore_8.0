using MediatR;

namespace Application.Features.Mediator.Commands.QuestionCommands
{
    public class CreateQuestionCommand : IRequest
    {
        public string Text { get; set; }
        public int Answer { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public int TestID { get; set; }
    }
}
