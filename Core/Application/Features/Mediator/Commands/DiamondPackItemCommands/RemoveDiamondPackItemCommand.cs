using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.DiamondPackItemCommands
{
    public class RemoveDiamondPackItemCommand : IRequest
    {
        public RemoveDiamondPackItemCommand(int ıd)
        {
            Id = ıd;
        }

        public int Id { get; set; }
    }
}
