using Application.Features.Mediator.Queries.ShopQuery;
using Application.Features.Mediator.Results.ShopResults;
using Application.Interfaces.IShopRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ShopHandlers
{
    public class GetShopItemsQueryHandler : IRequestHandler<GetShopItemsQuery, List<GetShopItemsQueryResult>>
    {
        private readonly IShopRepository _repository;

        public GetShopItemsQueryHandler(IShopRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetShopItemsQueryResult>> Handle(GetShopItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetAllAsync();

            var results = items.Select(item => new GetShopItemsQueryResult
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Color = item.Color,
                ImageUrl = item.ImageUrl
            }).ToList();

            return results;
        }
    }


}
