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

        // Yabancı Anahtar
        public int TopicID { get; set; } // Hangi konuya ait
        public Topic Topic { get; set; } // İlişki

        // Bire-Çoğul İlişki: Test -> Question
        public ICollection<Question> Questions { get; set; }
    }

}
