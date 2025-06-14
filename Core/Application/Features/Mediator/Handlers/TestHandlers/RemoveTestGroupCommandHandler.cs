using Application.Features.Mediator.Commands.TestCommand;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class RemoveTestGroupCommandHandler : IRequestHandler<RemoveTestGroupCommand>
    {
        private readonly IRepository<TestGroup> _repository;

        public RemoveTestGroupCommandHandler(IRepository<TestGroup> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveTestGroupCommand request, CancellationToken cancellationToken)
        {
            var testGroup = await _repository.GetByIdAsync(request.TestGroupID);
            if (testGroup != null)
            {
                await _repository.RemoveAsync(testGroup);
            }
        }
    }
}
