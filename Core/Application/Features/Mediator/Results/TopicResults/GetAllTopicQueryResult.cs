namespace Application.Features.Mediator.Results.TopicResults
{
    public class GetAllTopicQueryResult
    {
        public int TopicID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CourseID { get; set; }
    }
}
