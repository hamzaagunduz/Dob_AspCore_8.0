using Application.Features.Mediator.Commands.TestCommand;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class RemoveTestCommandHandler : IRequestHandler<RemoveTestCommand>
    {
        private readonly IRepository<Test> _repository;

        public RemoveTestCommandHandler(IRepository<Test> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _repository.GetByIdAsync(request.TestID);
            if (test != null)
            {
                await _repository.RemoveAsync(test);
            }
        }
    }
}
