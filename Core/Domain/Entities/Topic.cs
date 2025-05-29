using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Entities
{
    public class Topic
    {
        public int TopicID { get; set; } // Birincil Anahtar
        public string Name { get; set; } // Örneğin: Temel Aritmetik, Sayı Sistemleri
        public string? Description { get; set; }


        // Yabancı Anahtar
        public int CourseID { get; set; } // Hangi derse ait
        public Course Course { get; set; } // İlişki

        // Bire-Çoğul İlişki: Topic -> Test
        public ICollection<Test> Tests { get; set; }
        public ICollection<UserTopicPerformance> TopicPerformances { get; set; }

    }

}
