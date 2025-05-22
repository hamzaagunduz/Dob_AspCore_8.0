using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.DailyMissionResult
{
    public class UserDailyMissionResult
    {
        public int DailyMissionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TargetValue { get; set; }
        public int CurrentValue { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Date { get; set; }
    }
}
