using Application.Interfaces;
using Application.Features.Mediator.Results.TestResults;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.TestQueries;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, GetTestByIdQueryResult>
    {
        private readonly IRepository<Test> _repository;

        public GetTestByIdQueryHandler(IRepository<Test> repository)
        {
            _repository = repository;
        }

        public async Task<GetTestByIdQueryResult> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _repository.GetByIdAsync(request.TestID);
            if (test != null)
            {
                return new GetTestByIdQueryResult
                {
                    TestID = test.TestID,
                    Title = test.Title,
                    Description = test.Description,
                };
            }
            return null;
        }
    }
}
