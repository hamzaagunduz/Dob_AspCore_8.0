using Application.Features.Mediator.Queries.ExamQueries;
using Application.Features.Mediator.Results.ExamResults;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ExamHandlers
{
    public class GetExamByIdCommandHandler : IRequestHandler<GetExamByIdQuery, GetExamByIdQueryResult>
    {

        private readonly IRepository<Exam> _repository;

        public GetExamByIdCommandHandler(IRepository<Exam> repository)
        {
            _repository = repository;
        }

        public async Task<GetExamByIdQueryResult> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.id);
            return new GetExamByIdQueryResult
            {
                ExamID = values.ExamID,
                Name = values.Name,
                Year = values.Year
            };
        }
    }
}
