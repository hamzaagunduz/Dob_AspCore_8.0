using Application.Features.Mediator.Results.StatisticsResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.StatisticsQuery
{
    public class GetUserProfileStatisticsQuery : IRequest<UserProfileStatisticsResult>
    {
        public int AppUserId { get; set; }

        public GetUserProfileStatisticsQuery(int appUserId)
        {
            AppUserId = appUserId;
        }
    }
}
