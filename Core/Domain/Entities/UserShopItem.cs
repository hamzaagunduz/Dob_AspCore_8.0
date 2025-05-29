using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserShopItem
    {
        public int Id { get; set; } // Primary Key

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int ShopItemId { get; set; }
        public ShopItem ShopItem { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        public DateTime ExpirationDate { get; set; } // Satın alım süresi dolma tarihi
    }
}
