using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FlashCard
    {
        public int FlashCardID { get; set; } // Birincil Anahtar
        public string Front { get; set; } // Kartın ön yüzü (örneğin, başlık veya ipucu)
        public string Back { get; set; } // Kartın arka yüzü (örneğin, açıklama veya bilgi)

        // Yabancı Anahtar
        public int QuestionID { get; set; } // Hangi soruya ait
        public Question Question { get; set; } // İlişki
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();

    }

}
