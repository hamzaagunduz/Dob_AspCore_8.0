﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.AppUserCommands
{
    public class UpdateAppUserExamCommand:IRequest
    {
        public int UserId { get; set; }

        public int? ExamID { get; set; } // Hangi Sınava Çalıştığı
    }
}
