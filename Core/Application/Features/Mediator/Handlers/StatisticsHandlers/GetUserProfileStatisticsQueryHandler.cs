using Application.Features.Mediator.Queries.StatisticsQuery;
using Application.Features.Mediator.Results.StatisticsResults;
using Application.Interfaces;
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
    public class GetUserProfileStatisticsQueryHandler : IRequestHandler<GetUserProfileStatisticsQuery, UserProfileStatisticsResult>
    {
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IUserStatisticsRepository _userStatisticsRepository;

        public GetUserProfileStatisticsQueryHandler(
            IUserStatisticsRepository userStatisticsRepository, IRepository<AppUser> appUserRepository)
        {
            _userStatisticsRepository = userStatisticsRepository;
            _appUserRepository = appUserRepository;
        }

        public async Task<UserProfileStatisticsResult> Handle(GetUserProfileStatisticsQuery request, CancellationToken cancellationToken)
        {
            var (user, stats) = await _userStatisticsRepository.GetUserAndStatisticsAsync(request.AppUserId);

            if (user == null)
                return null;

            return new UserProfileStatisticsResult
            {
                FirstName = user.FirstName,
                SurName = user.SurName,
                Email = user.Email,
                Lives = user.Lives,
                TotalScore = stats.TotalScore,
                TotalTestsCompleted = stats.TotalTestsCompleted,
                PerfectTestsCompleted = stats.PerfectTestsCompleted,
                Score = stats.Score,
                League = stats.League,
                ConsecutiveDays = stats.ConsecutiveDays
            };
        }
    }
}
