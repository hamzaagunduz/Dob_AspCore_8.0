using Application.Features.Mediator.Commands.QuestionCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.QuestionHandlers
{
    public class RemoveQuestionCommandHandler : IRequestHandler<RemoveQuestionCommand>
    {
        private readonly IRepository<Question> _repository;

        public RemoveQuestionCommandHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _repository.GetByIdAsync(request.QuestionID);
            if (question != null)
            {
                await _repository.RemoveAsync(question);
            }
        }
    }
}
