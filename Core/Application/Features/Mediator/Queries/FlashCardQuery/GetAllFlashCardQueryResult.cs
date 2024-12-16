namespace Application.Features.Mediator.Queries.FlashCardQuery
{
    public class GetAllFlashCardQueryResult
    {
        public int FlashCardID { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int QuestionID { get; set; }
    }
}
