using Application.Features.Mediator.Queries.ShopQuery;
using Application.Features.Mediator.Results.ShopResults;
using Application.Interfaces;
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
    public class GetShopItemsWithUserStatusQueryHandler : IRequestHandler<GetShopItemsWithUserStatusQuery, ShopItemsWithDiamondQueryResult>
    {
        private readonly IShopRepository _shopRepository;
        private readonly IRepository<AppUser> _userRepository;

        public GetShopItemsWithUserStatusQueryHandler(IShopRepository shopRepository, IRepository<AppUser> userRepository)
        {
            _shopRepository = shopRepository;
            _userRepository = userRepository;
        }

        public async Task<ShopItemsWithDiamondQueryResult> Handle(GetShopItemsWithUserStatusQuery request, CancellationToken cancellationToken)
        {
            var shopItems = await _shopRepository.GetAllAsync();
            var userShopItems = await _shopRepository.GetUserShopItemsByUserIdAsync(request.UserId);
            var user = await _userRepository.GetByIdAsync(request.UserId);

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

            return new ShopItemsWithDiamondQueryResult
            {
                UserDiamondCount = user?.Diamond ?? 0,
                Items = result
            };
        }

    }
}
