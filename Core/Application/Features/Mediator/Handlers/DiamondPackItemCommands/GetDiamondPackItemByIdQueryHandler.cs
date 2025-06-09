using Application.Features.Mediator.Queries.DiamondPackItemQuery;
using Application.Features.Mediator.Results.DiamondPackItemQueryResult;
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
    public class GetDiamondPackItemByIdQueryHandler : IRequestHandler<GetDiamondPackItemByIdQuery, DiamondPackItemQueryResult>
    {
        private readonly IRepository<DiamondPackItem> _repository;

        public GetDiamondPackItemByIdQueryHandler(IRepository<DiamondPackItem> repository)
        {
            _repository = repository;
        }

        public async Task<DiamondPackItemQueryResult> Handle(GetDiamondPackItemByIdQuery request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);

            if (value == null)
                return null;

            return new DiamondPackItemQueryResult
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Description,
                DiamondAmount = value.DiamondAmount,
                BonusPercentage = value.BonusPercentage,
                PriceInTL = value.PriceInTL,
                ImageUrl = value.ImageUrl
            };
        }
    }

}
