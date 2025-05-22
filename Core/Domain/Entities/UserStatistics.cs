using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserStatistics
    {
        [Key]
        public int UserStatisticId { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // Genel Toplam Puan
        public int TotalScore { get; set; } = 0;

        // Kaç test çözdü
        public int TotalTestsCompleted { get; set; } = 0;

        // Kaç testi hatasız bitirdi
        public int PerfectTestsCompleted { get; set; } = 0;

        // Ortalama skor
        public int Score { get; set; } = 0;

        // Lig bilgisi
        public string League { get; set; } = "Bronze";

        // Arka arkaya kullanım günleri
        public int ConsecutiveDays { get; set; } = 0;

        // Streak ardından yeni streak
        public int ConsecutiveDaysTemp { get; set; } = 0;

        // En son test çözdüğü yapılan tarih
        public DateTime LastTestDate { get; set; } = DateTime.UtcNow;
    }
}

