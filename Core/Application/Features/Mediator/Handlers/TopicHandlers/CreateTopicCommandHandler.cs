using Application.Features.Mediator.Commands.TopicCommands;
using Application.Interfaces;
using Application.Interfaces.ITopicRepository;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.TopicHandlers
{
    public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IRepository<Topic> _repository;

        public CreateTopicCommandHandler(ITopicRepository topicRepository, IRepository<Topic> repository)
        {
            _topicRepository = topicRepository;
            _repository = repository;
        }

        public async Task Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            // Sıralama bilgisi
            int maxOrder = await _topicRepository.GetMaxOrderByCourseIdAsync(request.CourseID);

            var topic = new Topic
            {
                Name = request.Name,
                Description = request.Description,
                VideoLink = request.VideoLink,
                CourseID = request.CourseID,
                Order = maxOrder + 1
            };

            // Konuyu oluştur
            await _repository.CreateAsync(topic);
        }
    }
}
