using Application.Features.Mediator.Results.ShopResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.ShopQuery
{
    public class GetShopItemsWithUserStatusQuery : IRequest<ShopItemsWithDiamondQueryResult>
    {


        public int UserId { get; set; }
    }
}
