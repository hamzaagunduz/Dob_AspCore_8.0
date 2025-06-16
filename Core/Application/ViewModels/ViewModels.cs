using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ViewModels
    {
        public record ChatRequestVM(string Prompt, string ConnectionId)
        {

        }

        public record TopicPerformanceVM(string Topic, int Correct, int Wrong, int Duration);

        public record AnalysisRequestVM(string AnalysisType, List<TopicPerformanceVM> Data);
    }
}
