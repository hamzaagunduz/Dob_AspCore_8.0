using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course
    {
        public int CourseID { get; set; } // Birincil Anahtar
        public string Name { get; set; } // Örneğin: Matematik, Tarih
        public string? Description { get; set; } 

        
        // Yabancı Anahtar
        public int ExamID { get; set; } // Hangi sınava ait
        public Exam Exam { get; set; } // İlişki

        // Bire-Çoğul İlişki: Course -> Topic
        public ICollection<Topic> Topics { get; set; }
    }

}
