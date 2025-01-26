using Application.Features.Mediator.Queries.CourseQueries;
using Application.Features.Mediator.Results.CourseResults;
using Application.Interfaces.ICourseRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.CourseHandlers
{
    public class GetCoursesByExamQueryIdHandler : IRequestHandler<GetCoursesByExamIdQuery, List<GetCoursesByExamIdQueryResult>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCoursesByExamQueryIdHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<GetCoursesByExamIdQueryResult>> Handle(GetCoursesByExamIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetCoursesByExamIdAsync(request.ExamID);

            return courses.Select(course => new GetCoursesByExamIdQueryResult
            {
                CourseID = course.CourseID,
                Name = course.Name,
                Description = course.Description,
                IconURL = course.IconURL,
                ExamID = course.ExamID
            }).ToList();
        }
    }
}
