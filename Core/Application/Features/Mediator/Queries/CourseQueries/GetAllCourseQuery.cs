﻿using Application.Features.Mediator.Results.CourseResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.CourseQueries
{
    public class GetAllCourseQuery : IRequest<List<GetAllCourseQueryResult>>
    {
    }
}