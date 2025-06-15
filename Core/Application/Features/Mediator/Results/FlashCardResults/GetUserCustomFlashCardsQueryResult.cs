using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.FlashCardResults
{
    public record GetUserCustomFlashCardsQueryResult
    {
        public int UserCustomFlashCardID { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int CourseID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
