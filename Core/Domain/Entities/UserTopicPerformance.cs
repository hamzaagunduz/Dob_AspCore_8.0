using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserTopicPerformance
    {
        public int Id { get; set; }

        // Kullanıcı
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // Konu
        public int TopicID { get; set; }
        public Topic Topic { get; set; }

        // Performans bilgileri
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
        public int DurationInMinutes { get; set; }

        // Bu test çözümünün zamanı (tarihi)
        public DateTime CompletedAt { get; set; }
    }

}
