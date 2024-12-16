using Application.Features.Mediator.Commands.QuestionCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.QuestionHandlers
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand>
    {
        private readonly IRepository<Question> _repository;

        public CreateQuestionCommandHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = new Question
            {
                Text = request.Text,
                Answer = request.Answer,
                OptionA = request.OptionA,
                OptionB = request.OptionB,
                OptionC = request.OptionC,
                OptionD = request.OptionD,
                OptionE = request.OptionE,
                TestID = request.TestID
            };

            await _repository.CreateAsync(question);
        }
    }
}
