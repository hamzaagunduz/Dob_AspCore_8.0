using Application.Features.Mediator.Commands.TestCommand;
using Application.Interfaces;
using Application.Interfaces.ITestGroupRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class UpdateTestGroupCommandHandler : IRequestHandler<UpdateTestGroupCommand, bool>
    {
        private readonly IRepository<TestGroup> _repository;

        public UpdateTestGroupCommandHandler(IRepository<TestGroup> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateTestGroupCommand request, CancellationToken cancellationToken)
        {
            var existingGroup = await _repository.GetByIdAsync(request.TestGroupID);
            if (existingGroup == null)
                return false;

            existingGroup.Title = request.Title;
            existingGroup.Description = request.Description;
            existingGroup.Order=request.Order;

            await _repository.UpdateAsync(existingGroup);
            return true;
        }
    }
}
