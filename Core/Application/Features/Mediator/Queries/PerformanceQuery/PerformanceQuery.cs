using Application.Features.Mediator.Results.PerformanceQueryResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.PerformanceQuery
{
    public class PerformanceQuery : IRequest<PerformanceQueryResult>
    {
        public int userId { get; set; }
        public string Range { get; set; } // "weekly", "monthly", "all"
    }

}
