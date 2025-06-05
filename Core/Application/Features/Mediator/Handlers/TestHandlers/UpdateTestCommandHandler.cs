using Application.Features.Mediator.Commands.TestCommand;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand>
    {
        private readonly IRepository<Test> _repository;

        public UpdateTestCommandHandler(IRepository<Test> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _repository.GetByIdAsync(request.TestID);
            if (test != null)
            {
                test.Title = request.Title;
                test.Description = request.Description;
                await _repository.UpdateAsync(test);
            }
        }
    }
}
