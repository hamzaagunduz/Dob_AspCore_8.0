using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class TopicPerformanceV2
    {
        public string Topic { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public int Duration { get; set; }
    }

    public class AnalysisRequestVMV2
    {
        public string AnalysisType { get; set; }
        public List<TopicPerformanceV2> Data { get; set; }
    }
}
