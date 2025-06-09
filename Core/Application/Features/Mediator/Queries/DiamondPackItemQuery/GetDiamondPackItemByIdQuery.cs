using Application.Features.Mediator.Results.DiamondPackItemQueryResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.DiamondPackItemQuery
{
    public class GetDiamondPackItemByIdQuery : IRequest<DiamondPackItemQueryResult>
    {
        public GetDiamondPackItemByIdQuery(int ıd)
        {
            Id = ıd;
        }

        public int Id { get; set; }
    }
}
