using Application.Features.Mediator.Commands.ExamCommands;
using Application.Interfaces.IExamRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ExamHandlers
{
    public class SelectExamCommandHandler : IRequestHandler<SelectExamCommand>
    {
        private readonly IExamRepository _examRepository;

        public SelectExamCommandHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task Handle(SelectExamCommand request, CancellationToken cancellationToken)
        {
            // ExamID'ye sahip sınavı al
            var exam = await _examRepository.GetByIdAsync(request.ExamID);

            // Eğer exam mevcut değilse exception fırlat
            if (exam == null)
            {
                throw new KeyNotFoundException($"Sınav ID {request.ExamID} bulunamadı.");
            }

            // Eğer bu sınav zaten seçiliyse (Selected == true) hiçbir işlem yapma

            await _examRepository.UpdateAsync(exam);

        }


    }


}

