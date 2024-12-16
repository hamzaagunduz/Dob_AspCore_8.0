using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.TestCommand
{
    public class RemoveTestCommand : IRequest
    {
        public int TestID { get; set; }

        public RemoveTestCommand(int testID)
        {
            TestID = testID;
        }
    }
}
