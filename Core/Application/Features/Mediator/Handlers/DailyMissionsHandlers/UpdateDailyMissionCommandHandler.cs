using Application.Features.Mediator.Commands.DailyMissionsCommands;
using Application.Interfaces.IUserDailyMissionRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.DailyMissionsHandlers
{
    public class UpdateDailyMissionCommandHandler : IRequestHandler<UpdateDailyMissionCommand>
    {
        private readonly IUserDailyMissionRepository _repository;

        public UpdateDailyMissionCommandHandler(IUserDailyMissionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDailyMissionCommand request, CancellationToken cancellationToken)
        {
            var missions = await _repository.GetUserDailyMissionsByUserId(request.AppUserId);

            foreach (var mission in missions)
            {
                switch (mission.DailyMission.MetricType)
                {
                    case 1: // Puan Görevi
                        int earnedPoints = 100 - (request.WrongAnswerCount * 10);
                        if (earnedPoints < 0) earnedPoints = 0;
                        mission.CurrentValue += earnedPoints;
                        if (mission.CurrentValue >= mission.DailyMission.TargetValue)
                            mission.IsCompleted = true;
                        break;

                    case 2: // Test Tamamlama
                        mission.CurrentValue += 1;
                        if (mission.CurrentValue >= mission.DailyMission.TargetValue)

                            mission.IsCompleted = true;
                        break;

                    case 3: // Hatasız Tamamlama
                        if (request.WrongAnswerCount == 0)
                        {
                            mission.CurrentValue += 1;
                            if (mission.CurrentValue >= mission.DailyMission.TargetValue)
                                mission.IsCompleted = true;
                        }
                        break;
                }

                mission.Date = DateTime.UtcNow;
                await _repository.UpdateAsync(mission);
            }

            return ;
        }
    }

}
