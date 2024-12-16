using Application.Features.Mediator.Commands.QuestionCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.QuestionHandlers
{
    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand>
    {
        private readonly IRepository<Question> _repository;

        public UpdateQuestionCommandHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _repository.GetByIdAsync(request.QuestionID);
            if (question != null)
            {
                question.Text = request.Text;
                question.Answer = request.Answer;
                question.OptionA = request.OptionA;
                question.OptionB = request.OptionB;
                question.OptionC = request.OptionC;
                question.OptionD = request.OptionD;
                question.OptionE = request.OptionE;
                question.TestID = request.TestID;
                await _repository.UpdateAsync(question);
            }
        }
    }
}
