using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.TopicResults
{
    public class GetTopicsWithTestsByCourseIdQueryQueryResult
    {
        public int TopicID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }

        public List<TestDto> Tests { get; set; }
    }

    public class TestDto
    {
        public int? Order { get; set; }

        public int TestID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}