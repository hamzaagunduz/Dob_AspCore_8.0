using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.ShopResults
{
    public class ShopItemWithUserStatusQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public int DurationInDays { get; set; }
        public bool IsPurchased { get; set; }
        public int RemainingDays { get; set; }
    }
}
