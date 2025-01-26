using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.CourseResults
{
    public class GetAllCourseQueryResult
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? IconURL { get; set; }

        public int ExamID { get; set; }
    }
}
