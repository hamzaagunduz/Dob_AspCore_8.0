using Application.Features.Mediator.Commands.CourseCommands;
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
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
    {
        private readonly IRepository<Course> _repository;

        public CreateCourseCommandHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Course
            {
                Name = request.Name,
                Description = request.Description,
                ExamID = request.ExamID
            });
        }
    }
}
