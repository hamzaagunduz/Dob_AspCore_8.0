namespace WebApi.Models
{
    public class TopicPerformance
    {
        public string Topic { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public int Duration { get; set; }
    }

    public class AnalysisRequest
    {
        public string AnalysisType { get; set; } // daily, weekly, etc.
        public List<TopicPerformance> Data { get; set; }
    }
}
