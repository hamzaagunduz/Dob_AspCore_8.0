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
    public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand>
    {

        private readonly IRepository<Exam> _repository;

        public UpdateExamCommandHandler(IRepository<Exam> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.ExamID);
            values.Name = request.Name;
            values.Year= request.Year;
            values.Order=request.Order;
            await _repository.UpdateAsync(values);
        }
    }
}
