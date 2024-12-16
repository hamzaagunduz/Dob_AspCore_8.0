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
    public class GetAllCourseCommandHandler : IRequestHandler<GetAllCourseQuery, List<GetAllCourseQueryResult>>
    {
        private readonly IRepository<Course> _repository;

        public GetAllCourseCommandHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllCourseQueryResult>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var courses = await _repository.GetAllAsync();
            return courses.Select(c => new GetAllCourseQueryResult
            {
                CourseID = c.CourseID,
                Name = c.Name,
                Description = c.Description,
                ExamID = c.ExamID
            }).ToList();
        }
    }
}
