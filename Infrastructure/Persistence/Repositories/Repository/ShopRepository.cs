using Application.Interfaces.IShopRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly DobContext _context;

        public ShopRepository(DobContext context)
        {
            _context = context;
        }

        public async Task<List<ShopItem>> GetAllAsync()
        {
            return await _context.ShopItems.ToListAsync();
        }

        public async Task<ShopItem> AddAsync(ShopItem item)
        {
            _context.ShopItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task AddUserShopItemAsync(UserShopItem userShopItem)
        {
            _context.UserShopItems.Add(userShopItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserShopItem>> GetUserShopItemsByUserIdAsync(int userId)
        {
            return await _context.UserShopItems
                .Where(x => x.AppUserId == userId && x.ExpirationDate > DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<bool> HasActiveShopItemAsync(int userId, int shopItemId)
        {
            var currentDate = DateTime.UtcNow;

            return await _context.UserShopItems
                .Include(usi => usi.ShopItem)
                .AnyAsync(usi =>
                    usi.AppUserId == userId &&
                    usi.ShopItem.Id == shopItemId &&
                    usi.ExpirationDate > currentDate
                );
        }
    }

}
