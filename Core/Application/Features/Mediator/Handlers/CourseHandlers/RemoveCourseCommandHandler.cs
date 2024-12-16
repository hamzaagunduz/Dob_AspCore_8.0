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
    public class RemoveCourseCommandHandler : IRequestHandler<RemoveCourseCommand>
    {
        private readonly IRepository<Course> _repository;

        public RemoveCourseCommandHandler(IRepository<Course> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
        {
            var value = await _repository.GetByIdAsync(request.Id);
            if (value != null)
            {
                await _repository.RemoveAsync(value);
            }
        }
    }
}
