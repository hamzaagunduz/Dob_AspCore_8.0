﻿using Application.Features.Mediator.Results.AppUserResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.AppUserQueries
{
    public class GetAllAppUserQuery:IRequest<List<GetAllAppUserQueryResult>>
    {
    }
}
