using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.ExamCommands
{
    public class RemoveExamCommand : IRequest
    {
        public RemoveExamCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
