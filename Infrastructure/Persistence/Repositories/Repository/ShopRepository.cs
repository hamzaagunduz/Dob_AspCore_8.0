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
    }

}
