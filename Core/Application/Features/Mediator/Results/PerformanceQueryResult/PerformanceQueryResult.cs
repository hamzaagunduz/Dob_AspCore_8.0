using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.PerformanceQueryResult
{
    public class PerformanceQueryResult
    {
        public Dictionary<string, List<TopicPerformanceDto>> Courses { get; set; } // Key: CourseName
    }

    public class TopicPerformanceDto
    {
        public string Name { get; set; } // Topic Name
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public int Time { get; set; } // minutes
    }

}
