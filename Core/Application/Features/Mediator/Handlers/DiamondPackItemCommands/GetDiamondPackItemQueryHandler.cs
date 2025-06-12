using Application.Features.Mediator.Queries.DiamondPackItemQuery;
using Application.Features.Mediator.Results.DiamondPackItemQueryResult;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.DiamondPackItemCommands
{
    public class GetDiamondPackItemQueryHandler : IRequestHandler<GetDiamondPackItemQuery, List<DiamondPackItemQueryResult>>
    {
        private readonly IRepository<DiamondPackItem> _repository;

        public GetDiamondPackItemQueryHandler(IRepository<DiamondPackItem> repository)
        {
            _repository = repository;
        }

        public async Task<List<DiamondPackItemQueryResult>> Handle(GetDiamondPackItemQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();

            return values.Select(value => new DiamondPackItemQueryResult
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Description,
                DiamondAmount = value.DiamondAmount,
                BonusPercentage = value.BonusPercentage,
                PriceInTL = value.PriceInTL,
                ImageUrl = value.ImageUrl
            }).ToList();
        }
    }
}