using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.TopicResults
{
    public class GetTopicsWithGroupedTestsQueryResult
    {
        public int TopicID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? VideoLink { get; set; }

        public List<TestGroupDto> TestGroups { get; set; }
    }

    public class TestGroupDto
    {
        public int TestGroupID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<TestDto> Tests { get; set; }
    }


}
