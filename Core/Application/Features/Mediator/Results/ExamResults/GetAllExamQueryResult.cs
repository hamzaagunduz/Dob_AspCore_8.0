using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.ExamResults
{
    public class GetAllExamQueryResult
    {
        public int ExamID { get; set; }
        public string Name { get; set; }
        public bool? Selected { get; set; } // Örneğin: YKS, ALES

        public DateTime? Year { get; set; }
    }
}
