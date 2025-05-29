using Application.Features.Mediator.Results.ShopResults;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.ShopQuery
{
    public record GetShopItemsQuery : IRequest<List<GetShopItemsQueryResult>>;

}
