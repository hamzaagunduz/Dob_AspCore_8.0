using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.FlashCardResults
{
    public class GetUserFavoriteFlashCardsByCourseResult
    {
        public int FlashCardID { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int QuestionID { get; set; }
    }

    public class CombinedFlashCardResult
    {
        public int? FlashCardID { get; set; }
        public int? UserCustomFlashCardID { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int? QuestionID { get; set; }
        public string Type { get; set; } // "System" veya "Custom"
    }
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
