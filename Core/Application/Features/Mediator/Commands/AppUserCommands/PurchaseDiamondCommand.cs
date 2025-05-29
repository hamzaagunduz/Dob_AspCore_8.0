using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.AppUserCommands
{
    public class PurchaseDiamondCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int DiamondCount { get; set; }
        public decimal Amount { get; set; } // Satın alınan elmas karşılığı tutar
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CVV { get; set; }
    }
}
