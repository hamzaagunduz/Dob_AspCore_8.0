using Application.Features.Mediator.Queries.CourseQueries;
using Application.Features.Mediator.Results.CourseResults;
using Application.Interfaces;
using Application.Interfaces.ICourseRepository;
using Domain.Entities;
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
        private readonly IRepository<AppUser> _userRepository;

        public GetCoursesByExamQueryIdHandler(ICourseRepository courseRepository, IRepository<AppUser> userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<List<GetCoursesByExamIdQueryResult>> Handle(GetCoursesByExamIdQuery request, CancellationToken cancellationToken)
        {
            // request.ExamID artık aslında userId olacak
            var user = await _userRepository.GetByIdAsync(request.userId);

            if (user == null || user.ExamID == null)
                return new List<GetCoursesByExamIdQueryResult>();

            var courses = await _courseRepository.GetCoursesByExamIdAsync(user.ExamID.Value);

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
