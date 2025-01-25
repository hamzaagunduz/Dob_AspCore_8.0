using Application.Features.Mediator.Results.CourseResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.CourseQueries
{
    public class GetCoursesByExamIdQuery : IRequest<List<GetCoursesByExamIdQueryResult>>
    {
        public int ExamID { get; set; }

        public GetCoursesByExamIdQuery(int examId)
        {
            ExamID = examId;
        }
    }
}
