using Application.Features.Mediator.Commands.TestCommand;
using Application.Interfaces;
using Application.Interfaces.ITestGroupRepository;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TestHandlers
{
    public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand>
    {
        private readonly IRepository<Test> _repository;
        private readonly ITestGroupRepository _testGroupRepository;

        public CreateTestCommandHandler(IRepository<Test> repository, ITestGroupRepository testGroupRepository)
        {
            _repository = repository;
            _testGroupRepository = testGroupRepository;
        }

        public async Task Handle(CreateTestCommand request, CancellationToken cancellationToken)
        {
            // TestGroupID için mevcut max Order'ı al
            var maxOrder = await _testGroupRepository.GetMaxOrderByTestGroupIdAsync(request.TestGruopID);

            var test = new Test
            {
                Title = request.Title,
                Description = request.Description,
                TestGroupID = request.TestGruopID,
                Order = maxOrder + 1  // En sona eklemek için maxOrder + 1 yapıyoruz
            };

            await _repository.CreateAsync(test);
        }
    }
}
