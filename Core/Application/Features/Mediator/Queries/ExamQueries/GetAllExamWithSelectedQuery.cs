using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Mediator.Results.ExamResults;
using MediatR;

namespace Application.Features.Mediator.Queries.ExamQueries
{

    public class GetAllExamWithSelectedQuery : IRequest<List<GetAllExamWithSelectedQueryResult>>
    {
        public int UserId { get; set; }
    }

}
