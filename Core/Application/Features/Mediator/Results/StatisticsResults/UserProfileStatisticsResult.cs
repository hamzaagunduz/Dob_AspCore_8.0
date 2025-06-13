using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.StatisticsResults
{
    public class UserProfileStatisticsResult
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Lives { get; set; }
        public int? Diamond { get; set; }

        public int TotalScore { get; set; }
        public int TotalTestsCompleted { get; set; }
        public int PerfectTestsCompleted { get; set; }
        public int Score { get; set; }
        public string League { get; set; }
        public int ConsecutiveDays { get; set; }
    }

}
