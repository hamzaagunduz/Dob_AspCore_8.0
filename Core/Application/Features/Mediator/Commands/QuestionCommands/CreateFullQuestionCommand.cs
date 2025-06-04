using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.QuestionCommands
{
    public class CreateFullQuestionCommand : IRequest<int>
    {
        public string QuestionText { get; set; }

        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }
        public int TestId { get; set; }
        public int Answer { get; set; }

        public IFormFile? QuestionImage { get; set; }
        public IFormFile? OptionAImage { get; set; }
        public IFormFile? OptionBImage { get; set; }
        public IFormFile? OptionCImage { get; set; }
        public IFormFile? OptionDImage { get; set; }
        public IFormFile? OptionEImage { get; set; }
    }
}
