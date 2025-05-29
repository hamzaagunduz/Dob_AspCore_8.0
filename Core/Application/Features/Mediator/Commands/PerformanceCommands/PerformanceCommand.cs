using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.PerformanceCommands
{
    public class PerformanceCommand : IRequest
    {
        public int AppUserId { get; set; }
        public DateTime CompletedAt { get; set; }
        public List<PerformanceCommandItem> Performances { get; set; }

        public class PerformanceCommandItem
        {
            public int TopicId { get; set; }
            public int CorrectCount { get; set; }
            public int WrongCount { get; set; }
            public int DurationInMinutes { get; set; }
        }
    }

}
