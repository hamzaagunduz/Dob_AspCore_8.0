using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.TestCommand
{
    public class CreateTestGroupCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        public int? TopicID { get; set; }
    }
}
