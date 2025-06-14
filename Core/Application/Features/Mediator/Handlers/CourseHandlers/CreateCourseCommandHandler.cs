using Application.Features.Mediator.Commands.CourseCommands;
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
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
    {
        private readonly IRepository<Course> _repository;
        private readonly ICourseRepository _courseRepository;

        public CreateCourseCommandHandler(IRepository<Course> repository, ICourseRepository courseRepository)
        {
            _repository = repository;
            _courseRepository = courseRepository;
        }

        public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            // Önce mevcut en yüksek Order değerini al
            var maxOrder = await _courseRepository.GetMaxOrderByExamIdAsync(request.ExamID);

            var newCourse = new Course
            {
                Name = request.Name,
                Description = request.Description,
                IconURL = request.IconURL,
                ExamID = request.ExamID,
                Order = maxOrder + 1  // Bir sonrakini veriyoruz
            };

            await _repository.CreateAsync(newCourse);
        }
    }
}
