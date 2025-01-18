using MediatR;
using System;

namespace Application.Features.Mediator.Commands.ExamCommands
{
    public class SelectExamCommand : IRequest
    {
        public int ExamID { get; set; }
    }
}
