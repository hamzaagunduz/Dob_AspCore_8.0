using Application.Features.Mediator.Results.TopicResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.TopicQueries
{
    public class GetTopicsWithTestsByCourseIdQuery : IRequest<List<GetTopicsWithTestsByCourseIdQueryQueryResult>>
    {
        
        public int CourseId { get; set; }

        public GetTopicsWithTestsByCourseIdQuery(int CourseId)
        {
            this.CourseId = CourseId;
        }
    }
}
