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
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
    {
        private readonly IRepository<Course> _repository;

        public UpdateCourseCommandHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _repository.GetByIdAsync(request.CourseID);
            if (course != null)
            {
                course.Name = request.Name;
                course.Description = request.Description;
                course.IconURL = request.IconURL;
                await _repository.UpdateAsync(course);
            }
        }
    }
}
