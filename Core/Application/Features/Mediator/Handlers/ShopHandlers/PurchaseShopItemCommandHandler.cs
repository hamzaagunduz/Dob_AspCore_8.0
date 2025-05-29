using Application.Features.Mediator.Commands.ShopCommands;
using Application.Interfaces.IShopRepository;
using Application.Interfaces.IUserRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ShopHandlers
{
    public class PurchaseShopItemCommandHandler : IRequestHandler<PurchaseShopItemCommand, string>
    {
        private readonly IShopRepository _shopRepository;
        private readonly IUserRepository _userRepository;

        public PurchaseShopItemCommandHandler(IShopRepository shopRepository, IUserRepository userRepository)
        {
            _shopRepository = shopRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(PurchaseShopItemCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null) return "Kullanıcı bulunamadı.";

            var shopItem = (await _shopRepository.GetAllAsync())
                .FirstOrDefault(x => x.Id == request.ShopItemId);
            if (shopItem == null) return "Ürün bulunamadı.";

            if (user.Diamond < shopItem.Price) return "Yetersiz elmas.";

            // Elmas düş
            user.Diamond -= shopItem.Price;
            await _userRepository.UpdateAsync(user);

            // Kullanıcıya ürün ata (1 ay geçerli olacak)
            var userShopItem = new UserShopItem
            {
                AppUserId = user.Id,
                ShopItemId = shopItem.Id,
                PurchaseDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(shopItem.DurationInDays)
            };


            await _shopRepository.AddUserShopItemAsync(userShopItem);

            return "Satın alma başarılı.";
        }
    }
}
