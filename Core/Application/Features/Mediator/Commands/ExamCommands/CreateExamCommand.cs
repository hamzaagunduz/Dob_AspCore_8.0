﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.ExamCommands
{
    public class CreateExamCommand : IRequest
    {


        public string Name { get; set; }
        public DateTime? Year { get; set; }
    }
}
