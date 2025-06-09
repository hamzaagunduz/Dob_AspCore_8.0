using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.DiamondPackItemCommands
{
    public class CreateDiamondPackItemCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DiamondAmount { get; set; }
        public int BonusPercentage { get; set; }
        public int PriceInTL { get; set; }
        public string ImageUrl { get; set; }
    }
}
