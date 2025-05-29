using Application.Features.Mediator.Queries.ShopQuery;
using Application.Features.Mediator.Results.ShopResults;
using Application.Interfaces.IShopRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ShopHandlers
{
    public class GetShopItemsWithUserStatusQueryHandler : IRequestHandler<GetShopItemsWithUserStatusQuery, List<ShopItemWithUserStatusQueryResult>>
    {
        private readonly IShopRepository _shopRepository;

        public GetShopItemsWithUserStatusQueryHandler(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<List<ShopItemWithUserStatusQueryResult>> Handle(GetShopItemsWithUserStatusQuery request, CancellationToken cancellationToken)
        {
            var shopItems = await _shopRepository.GetAllAsync();

            // Kullanıcının satın aldığı ürünleri UserShopItem tablosundan çekiyoruz
            var userShopItems = await _shopRepository.GetUserShopItemsByUserIdAsync(request.UserId);

            var result = shopItems.Select(item =>
            {
                var purchasedItem = userShopItems.FirstOrDefault(x => x.ShopItemId == item.Id && x.ExpirationDate > DateTime.UtcNow);

                return new ShopItemWithUserStatusQueryResult
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Color = item.Color,
                    ImageUrl = item.ImageUrl,
                    DurationInDays = item.DurationInDays,
                    IsPurchased = purchasedItem != null,
                    RemainingDays = purchasedItem != null ? (int)(purchasedItem.ExpirationDate - DateTime.UtcNow).TotalDays : 0
                };
            }).ToList();

            return result;
        }
    }
}
