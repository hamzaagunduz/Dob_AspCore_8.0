using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Exam
    {
        public int ExamID { get; set; } // Birincil Anahtar
        public string Name { get; set; } // Örneğin: YKS, ALES
        public bool? Selected { get; set; } // Örneğin: YKS, ALES
        public DateTime? Year { get; set; } 

        // Bire-Çoğul İlişki: Exam -> Course
        public ICollection<Course> Courses { get; set; }
    }


}
