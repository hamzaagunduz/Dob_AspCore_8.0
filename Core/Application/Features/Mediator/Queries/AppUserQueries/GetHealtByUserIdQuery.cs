using Application.Features.Mediator.Results.AppUserResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.AppUserQueries
{
    public class GetHealtByUserIdQuery:IRequest<GetHealtByUserIdQueryResult>
    {
        public GetHealtByUserIdQuery(int id)
        {
            this.id = id;
        }

        public int id { get; set; }

    }
}
