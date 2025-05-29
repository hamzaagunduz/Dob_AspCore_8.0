using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.ShopResults
{
    public class GetShopItemsQueryResult
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; } // Diamond olarak fiyat
        public string Color { get; set; } // "orange", "purple" gibi
        public string ImageUrl { get; set; } // Görsel yolu
    }
}
