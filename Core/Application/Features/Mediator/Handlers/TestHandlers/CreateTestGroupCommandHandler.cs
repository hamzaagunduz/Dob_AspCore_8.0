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
            var maxOrder = await _repository.GetMaxOrderByTopicIdAsync(request.TopicID);

            var testGroup = new TestGroup
            {
                Title = request.Title,
                Description = request.Description,
                TopicID = request.TopicID,
                Order = maxOrder + 1 // En sona ekle
            };

            var created = await _repository.AddAsync(testGroup);
            return created.TestGroupID;
        }
    }
}
