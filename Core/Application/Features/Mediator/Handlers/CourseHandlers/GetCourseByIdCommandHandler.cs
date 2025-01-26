using Application.Features.Mediator.Queries.CourseQueries;
using Application.Features.Mediator.Results.CourseResults;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.CourseHandlers
{
    public class GetCourseByIdCommandHandler : IRequestHandler<GetCourseByIdQuery, GetCourseByIdQueryResult>
    {
        private readonly IRepository<Course> _repository;

        public GetCourseByIdCommandHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public async Task<GetCourseByIdQueryResult> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _repository.GetByIdAsync(request.id);
            if (course == null)
            {
                return null;
            }

            return new GetCourseByIdQueryResult
            {
                CourseID = course.CourseID,
                Name = course.Name,
                Description = course.Description,
                IconURL=course.IconURL,
                ExamID = course.ExamID
            };
        }
    }
}
