using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.CourseCommands
{
    public class CreateCourseCommand : IRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? IconURL { get; set; }

        public int ExamID { get; set; }
    }
}
