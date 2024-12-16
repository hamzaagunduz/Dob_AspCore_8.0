namespace Application.Features.Mediator.Results.QuestionResults
{
    public class GetQuestionByIdQueryResult
    {
        public int QuestionID { get; set; }
        public string Text { get; set; }
        public int Answer { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public int TestID { get; set; }
    }
}
