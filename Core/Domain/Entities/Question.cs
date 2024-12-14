using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Question
    {
        public int QuestionID { get; set; } // Birincil Anahtar
        public string Text { get; set; } // Sorunun metni
        public string Answer { get; set; } // Doğru cevap
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }

        // Yabancı Anahtar
        public int TestID { get; set; } // Hangi teste ait
        public Test Test { get; set; } // İlişki
        public FlashCard FlashCard { get; set; }

    }

}
