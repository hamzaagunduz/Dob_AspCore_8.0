using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Test
    {
        public int TestID { get; set; } // Birincil Anahtar
        public string Title { get; set; } // Test başlığı
        public string? Description { get; set; } // Test başlığı
        public string? Description2 { get; set; } // Test başlığı

        public int? Order { get; set; }
        public int? TestGroupID { get; set; }
        public TestGroup? TestGroup { get; set; }
        public ICollection<Question> Questions { get; set; }
    }

}
