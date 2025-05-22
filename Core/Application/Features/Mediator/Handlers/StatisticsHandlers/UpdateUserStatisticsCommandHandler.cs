using Application.Features.Mediator.Commands.StatisticsCommands;
using Application.Interfaces.IUserStatisticsRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.StatisticsHandlers
{
    public class UpdateUserStatisticsCommandHandler : IRequestHandler<UpdateUserStatisticsCommand>
    {
        private readonly IUserStatisticsRepository _statisticsRepository;

        public UpdateUserStatisticsCommandHandler(IUserStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task Handle(UpdateUserStatisticsCommand request, CancellationToken cancellationToken)
        {
            var stats = await _statisticsRepository.GetByUserIdAsync(request.AppUserId);
            if (stats == null)
            {
                stats = new UserStatistics
                {
                    AppUserId = request.AppUserId
                };

                await _statisticsRepository.AddAsync(stats);
            }

            int score = Math.Max(0, 100 - request.WrongAnswerCount * 10);
            stats.TotalScore += score;
            stats.TotalTestsCompleted += 1;

            if (request.WrongAnswerCount == 0)
            {
                stats.PerfectTestsCompleted += 1;
            }

            stats.Score = stats.TotalScore / stats.TotalTestsCompleted;
            stats.League = CalculateLeague(stats.TotalScore);

            DateTime now = DateTime.UtcNow;
            TimeSpan difference = now.Date - stats.LastTestDate.Date;
            if (difference.TotalDays >= 1 && difference.TotalDays <= 2)
            {

                stats.ConsecutiveDaysTemp += 1;
                if (stats.ConsecutiveDaysTemp > stats.ConsecutiveDays)
                {
                    stats.ConsecutiveDays = stats.ConsecutiveDaysTemp;
                }
                stats.LastTestDate = now;

            }
            else if (difference.TotalDays >= 2)
            {
                stats.ConsecutiveDaysTemp = 0;
                stats.LastTestDate = now;

            }


            await _statisticsRepository.UpdateAsync(stats);

            return;
        }

        private string CalculateLeague(int totalScore)
        {
            if (totalScore >= 1000) return "Diamond";
            if (totalScore >= 500) return "Gold";
            if (totalScore >= 250) return "Silver";
            return "Bronze";
        }
    }
}
