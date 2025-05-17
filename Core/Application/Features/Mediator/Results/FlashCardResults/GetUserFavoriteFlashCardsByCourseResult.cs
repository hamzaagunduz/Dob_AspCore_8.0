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
}
