using Application.Features.Mediator.Queries.DailyMissionsQuery;
using Application.Features.Mediator.Results.DailyMissionResult;
using Application.Interfaces.IUserDailyMissionRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.DailyMissionsHandlers
{
    public class GetUserDailyMissionsQueryHandler : IRequestHandler<GetUserDailyMissionsQuery, List<UserDailyMissionResult>>
    {
        private readonly IUserDailyMissionRepository _repository;

        public GetUserDailyMissionsQueryHandler(IUserDailyMissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserDailyMissionResult>> Handle(GetUserDailyMissionsQuery request, CancellationToken cancellationToken)
        {
            var tupleData = await _repository.GetUserDailyMissionResultsByUserId(request.userId);

            var result = tupleData.Select(x => new UserDailyMissionResult
            {
                DailyMissionId = x.DailyMissionId,
                Title = x.Title,
                Description = x.Description,
                TargetValue = x.TargetValue,
                CurrentValue = x.CurrentValue,
                IsCompleted = x.IsCompleted,
                Date = x.Date
            }).ToList();

            return result;
        }
    }
}
