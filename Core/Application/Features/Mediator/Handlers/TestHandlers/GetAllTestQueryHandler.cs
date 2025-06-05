using Application.Interfaces;
using Application.Features.Mediator.Results.TestResults;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.TestQueries;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class GetAllTestQueryHandler : IRequestHandler<GetAllTestQuery, List<GetAllTestQueryResult>>
    {
        private readonly IRepository<Test> _repository;

        public GetAllTestQueryHandler(IRepository<Test> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllTestQueryResult>> Handle(GetAllTestQuery request, CancellationToken cancellationToken)
        {
            var tests = await _repository.GetAllAsync();
            return tests.Select(test => new GetAllTestQueryResult
            {
                TestID = test.TestID,
                Title = test.Title,
                Description = test.Description,
            }).ToList();
        }
    }
}
