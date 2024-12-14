using Application.Features.Mediator.Results.ExamResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.ExamQueries
{
    public class GetAllExamQuery : IRequest<List<GetAllExamQueryResult>>
    {
    }
}
