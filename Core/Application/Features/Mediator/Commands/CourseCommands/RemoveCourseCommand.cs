﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.CourseCommands
{
    public class RemoveCourseCommand : IRequest
    {
        public RemoveCourseCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}