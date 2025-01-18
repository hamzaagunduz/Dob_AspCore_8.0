using Application.Features.Mediator.Commands.ExamCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ExamHandlers
{
    public class CreateExamHandler : IRequestHandler<CreateExamCommand>
    {

        private readonly IRepository<Exam> _repository;

        public CreateExamHandler()
        {
        }

        public CreateExamHandler(IRepository<Exam> repository)
        {
            _repository = repository;
        }
        public async Task Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Exam
            {
                Name = request.Name,
                Selected=request.Selected,
                Year = request.Year
            });
        }
    }


}
