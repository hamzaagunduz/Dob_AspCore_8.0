using Application.Features.Mediator.Commands.TestCommand;
using Application.Interfaces;
using Application.Interfaces.ITestGroupRepository;
using Domain.Entities;
using MediatR;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class CreateTestGroupCommandHandler : IRequestHandler<CreateTestGroupCommand, int>
    {
        private readonly ITestGroupRepository _repository;

        public CreateTestGroupCommandHandler(ITestGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateTestGroupCommand request, CancellationToken cancellationToken)
        {
            var testGroup = new TestGroup
            {
                Title = request.Title,
                Description = request.Description,
                TopicID = request.TopicID,
                test=request.test,
            };

            var created = await _repository.AddAsync(testGroup);
            return created.TestGroupID;
        }
    }
}
