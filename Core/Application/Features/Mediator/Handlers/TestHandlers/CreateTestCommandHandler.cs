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
    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand>
    {
        private readonly IRepository<Test> _repository;

        public CreateTestCommandHandler(IRepository<Test> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            var test = new Test
            {
                Title = request.Title,
                Description = request.Description,
                TestGroupID = request.TestGruopID,
            };

            await _repository.CreateAsync(test);
        }
    }
}
