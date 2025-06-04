using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Text { get; set; }
        public int Answer { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? OptionE { get; set; }

        public int TestID { get; set; }
        public Test Test { get; set; }

        public FlashCard? FlashCard { get; set; }

        public ICollection<QuestionImage>? Images { get; set; } // Resim olmayabilir
    }


}
