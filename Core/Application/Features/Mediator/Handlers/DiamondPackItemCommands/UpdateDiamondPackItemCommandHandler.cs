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
    public class UpdateDiamondPackItemCommandHandler : IRequestHandler<UpdateDiamondPackItemCommand>
    {
        private readonly IRepository<DiamondPackItem> _repository;

        public UpdateDiamondPackItemCommandHandler(IRepository<DiamondPackItem> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDiamondPackItemCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            if (value != null)
            {
                value.Name = request.Name;
                value.Description = request.Description;
                value.DiamondAmount = request.DiamondAmount;
                value.BonusPercentage = request.BonusPercentage;
                value.PriceInTL = request.PriceInTL;
                value.ImageUrl = request.ImageUrl;
                await _repository.UpdateAsync(value);
            }
        }
    }
}
