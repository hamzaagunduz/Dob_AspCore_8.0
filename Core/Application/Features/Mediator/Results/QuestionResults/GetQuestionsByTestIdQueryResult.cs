using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.QuestionResults
{
    public class GetQuestionsByTestIdQueryResult
    {
        public int QuestionID { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public int Answer { get; set; }

        // Yeni eklenen alan
        public List<QuestionImageDto> Images { get; set; } // 👈 yeni property
        public int Type { get; set; }

    }


    public class QuestionImageDto
    {
        public string ImageUrl { get; set; }
        public QuestionImageType? Type { get; set; }
    }

}
