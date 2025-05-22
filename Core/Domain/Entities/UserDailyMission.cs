using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserDailyMission
    {
        [Key]
        public int UserDailyMissionId { get; set; }
        public int AppUserId { get; set; }
        public int DailyMissionId { get; set; }
        public int CurrentValue { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime ? Date { get; set; }

        public DailyMission DailyMission { get; set; }
    }

}
