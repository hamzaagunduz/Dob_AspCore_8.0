using Application.Features.Mediator.Commands.DiamondPackItemCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.DiamondPackItemCommands
{
    public class RemoveDiamondPackItemCommandHandler : IRequestHandler<RemoveDiamondPackItemCommand>
    {
        private readonly IRepository<DiamondPackItem> _repository;

        public RemoveDiamondPackItemCommandHandler(IRepository<DiamondPackItem> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveDiamondPackItemCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}
