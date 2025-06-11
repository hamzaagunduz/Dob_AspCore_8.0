using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.DashboardQueryResult
{
    public class DashboardQueryResult
    {
        public int TotalUsers { get; set; }
        public int DailyActiveUsers { get; set; }
        public double AverageTestCompletion { get; set; }
        public int TotalDiamonds { get; set; }

        public SystemInfo SystemInfo { get; set; } = new();
        public List<WeeklyActiveUserData> WeeklyActiveUsers { get; set; } = new();

        public List<LastFiveUserDto> LastFiveUsers { get; set; } = new();


    }

    public class WeeklyActiveUserData
    {
        public string DayName { get; set; }
        public string ShortDayName { get; set; }
        public int Count { get; set; }
    }
    public class LastFiveUserDto
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
    }

    public class SystemInfo
    {
        public double CpuUsage { get; set; }
        public double MemoryUsage { get; set; }
        public double DiskUsage { get; set; }
        public double NetworkUsage { get; set; }
    }
}
