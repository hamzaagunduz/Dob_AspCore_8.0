using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IShopRepository
{
    public interface IShopRepository
    {
        Task<List<ShopItem>> GetAllAsync();
        Task<ShopItem> AddAsync(ShopItem item);
        Task AddUserShopItemAsync(UserShopItem userShopItem);
        Task<List<UserShopItem>> GetUserShopItemsByUserIdAsync(int userId);

    }

}
