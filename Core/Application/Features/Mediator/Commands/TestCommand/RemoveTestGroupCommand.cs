using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.TestCommand
{
    public class RemoveTestGroupCommand : IRequest
    {
        public int TestGroupID { get; set; }

        public RemoveTestGroupCommand(int testGroupID)
        {
            TestGroupID = testGroupID;
        }
    }
}
