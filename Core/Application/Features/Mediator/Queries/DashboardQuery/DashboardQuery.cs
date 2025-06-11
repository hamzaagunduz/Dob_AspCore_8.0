using Application.Features.Mediator.Results.DashboardQueryResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.DashboardQuery
{
    public class DashboardQuery : IRequest<DashboardQueryResult>
    {
    }
}
