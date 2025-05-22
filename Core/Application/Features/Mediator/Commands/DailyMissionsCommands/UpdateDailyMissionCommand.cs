using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.DailyMissionsCommands
{
    public class UpdateDailyMissionCommand : IRequest
    {
        public int AppUserId { get; set; }
        public int WrongAnswerCount { get; set; }
    }

}
