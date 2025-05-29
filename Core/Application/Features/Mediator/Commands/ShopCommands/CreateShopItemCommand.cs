using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.ShopCommands
{
    public class CreateShopItemCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
    }
}
