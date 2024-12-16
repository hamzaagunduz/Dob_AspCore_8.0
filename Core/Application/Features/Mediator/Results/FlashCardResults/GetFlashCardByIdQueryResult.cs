namespace Application.Features.Mediator.Results.FlashCardResults
{
    public class GetFlashCardByIdQueryResult
    {
        public int FlashCardID { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int QuestionID { get; set; }
    }
}
