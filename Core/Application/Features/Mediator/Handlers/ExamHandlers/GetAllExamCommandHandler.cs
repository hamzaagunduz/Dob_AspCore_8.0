using Application.Features.Mediator.Queries.ExamQueries;
using Application.Features.Mediator.Results.ExamResults;
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
    public class GetAllExamCommandHandler : IRequestHandler<GetAllExamQuery, List<GetAllExamQueryResult>>
    {
        private readonly IRepository<Exam> _repository;

        public GetAllExamCommandHandler(IRepository<Exam> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllExamQueryResult>> Handle(GetAllExamQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();

            return values
                .OrderBy(x => x.Order ?? int.MaxValue) // Order null ise en sona atar
                .Select(x => new GetAllExamQueryResult
                {
                    ExamID = x.ExamID,
                    Name = x.Name,
                    Year = x.Year,
                    Order=x.Order,
                })
                .ToList();
        }

    }
}
