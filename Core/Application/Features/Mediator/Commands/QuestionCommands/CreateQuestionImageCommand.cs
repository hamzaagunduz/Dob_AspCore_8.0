using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.QuestionCommands
{
    public class CreateQuestionImageCommand : IRequest<string>
    {
        public int QuestionID { get; set; }
        public IFormFile File { get; set; }
        public QuestionImageType Type { get; set; }
    }

}
