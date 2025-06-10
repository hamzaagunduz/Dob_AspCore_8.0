using Application.Interfaces;
using Application.Features.Mediator.Results.TestResults;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.TestQueries;
using Application.Interfaces.ITestGroupRepository;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, GetTestByIdQueryResult>
    {
        private readonly ITestGroupRepository _testGroupRepository;

        public GetTestByIdQueryHandler(ITestGroupRepository testGroupRepository)
        {
            _testGroupRepository = testGroupRepository;
        }

        public async Task<GetTestByIdQueryResult> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _testGroupRepository.GetTestWithGroupAndTopicAsync(request.TestID);

            if (test != null)
            {
                return new GetTestByIdQueryResult
                {
                    TestID = test.TestID,
                    Title = test.Title,
                    Description = test.Description,
                    TopicID = test.TestGroup?.TopicID
                };
            }

            return null;
        }
    }
}
