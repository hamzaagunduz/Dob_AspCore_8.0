using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DailyMission
    {
        public int Id { get; set; }
        public string Title { get; set; }  // Örn: "500 puan kazan"
        public string Description { get; set; }
        public int TargetValue { get; set; }  // Örn: 500
        public int MetricType { get; set; }  // 1: skor 2: tamamlama 3: hatasız tamamlama
    }

}
