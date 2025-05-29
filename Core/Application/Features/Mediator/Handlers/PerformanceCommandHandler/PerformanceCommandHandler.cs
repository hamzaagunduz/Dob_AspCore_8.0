using Application.Features.Mediator.Commands.PerformanceCommands;
using Application.Interfaces.IUserTopicPerformanceRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.PerformanceCommandHandler
{
    public class PerformanceCommandHandler : IRequestHandler<PerformanceCommand>
    {
        private readonly IUserTopicPerformanceRepository _performanceRepository;

        public PerformanceCommandHandler(IUserTopicPerformanceRepository performanceRepository)
        {
            _performanceRepository = performanceRepository;
        }

        public async Task Handle(PerformanceCommand request, CancellationToken cancellationToken)
        {

            var performances = request.Performances.Select(p => new UserTopicPerformance
            {
                AppUserId = request.AppUserId,
                TopicID = p.TopicId,
                CorrectCount = p.CorrectCount,
                WrongCount = p.WrongCount,
                DurationInMinutes = p.DurationInMinutes,
                CompletedAt = request.CompletedAt
            }).ToList();

            await _performanceRepository.AddRangeAsync(performances);
            await _performanceRepository.SaveChangesAsync();

            return ;
        }
    }

}
