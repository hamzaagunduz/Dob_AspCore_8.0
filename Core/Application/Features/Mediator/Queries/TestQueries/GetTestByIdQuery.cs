﻿using Application.Features.Mediator.Results.TestResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.TestQueries
{
    public class GetTestByIdQuery : IRequest<GetTestByIdQueryResult>
    {
        public int TestID { get; set; }

        public GetTestByIdQuery(int testID)
        {
            TestID = testID;
        }
    }
}