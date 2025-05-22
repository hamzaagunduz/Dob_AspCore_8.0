using Application.Features.Mediator.Results.DailyMissionResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.DailyMissionsQuery
{
    public class GetUserDailyMissionsQuery : IRequest<List<UserDailyMissionResult>>
    {
        public GetUserDailyMissionsQuery(int userId)
        {
            this.userId = userId;
        }

        public int userId { get; set; }
    }
}
