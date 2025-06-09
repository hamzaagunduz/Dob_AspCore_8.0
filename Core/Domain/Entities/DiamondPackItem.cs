using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DiamondPackItem
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }

        public int DiamondAmount { get; set; } // Örn: 100
        public int BonusPercentage { get; set; } // Örn: 5
        public int PriceInTL { get; set; } // Örn: ₺50

        public string ImageUrl { get; set; } // Görsel yolu
    }
}
