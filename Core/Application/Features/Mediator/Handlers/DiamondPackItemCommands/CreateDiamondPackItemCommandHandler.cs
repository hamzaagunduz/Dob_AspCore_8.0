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
    public class CreateDiamondPackItemCommandHandler : IRequestHandler<CreateDiamondPackItemCommand>
    {
        private readonly IRepository<DiamondPackItem> _repository;

        public CreateDiamondPackItemCommandHandler(IRepository<DiamondPackItem> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateDiamondPackItemCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new DiamondPackItem
            {
                Name = request.Name,
                Description = request.Description,
                DiamondAmount = request.DiamondAmount,
                BonusPercentage = request.BonusPercentage,
                PriceInTL = request.PriceInTL,
                ImageUrl = request.ImageUrl
            });
        }
    }
}
