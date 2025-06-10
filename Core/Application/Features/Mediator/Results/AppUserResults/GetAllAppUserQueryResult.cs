using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.AppUserResults
{
    public class GetAllAppUserQueryResult
    {
        // Existing properties
        public int UserId { get; set; }
        public string Email { get; set; }
        public string SurName { get; set; }
        public int? ExamID { get; set; }
        public string FirstName { get; set; }
        public string ImageURL { get; set; }
        public bool? Ban { get; set; }
        public int? Diamond { get; set; }
        public DateTime? LastLifeAddedTime { get; set; }

        // New properties from UserStatistics
        public int TotalScore { get; set; }
        public int TotalTestsCompleted { get; set; }
        public int PerfectTestsCompleted { get; set; }
        public int AverageScore { get; set; } // Renamed to avoid conflict
        public string League { get; set; }
        public int ConsecutiveDays { get; set; }
        public int ConsecutiveDaysTemp { get; set; }
        public DateTime LastTestDate { get; set; }
    }
}
