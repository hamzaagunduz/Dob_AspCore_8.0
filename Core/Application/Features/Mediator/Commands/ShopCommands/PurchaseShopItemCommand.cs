using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.ShopCommands
{
    public class PurchaseShopItemCommand : IRequest<string>
    {
        public int UserId { get; set; }
        public int ShopItemId { get; set; }
    }
}
