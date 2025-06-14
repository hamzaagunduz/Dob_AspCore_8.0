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
            // Mevcut Exam'ler arasındaki en yüksek Order değerini bul
            var exams = await _repository.GetAllAsync(); 
            int maxOrder = exams.Any() ? exams.Max(e => e.Order ?? 0) : 0;

            var newExam = new Exam
            {
                Name = request.Name,
                Year = request.Year,
                Order = maxOrder + 1
            };

            await _repository.CreateAsync(newExam);
        }

    }


}
