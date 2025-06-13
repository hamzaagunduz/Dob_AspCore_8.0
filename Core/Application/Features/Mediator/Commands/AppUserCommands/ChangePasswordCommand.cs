using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.AppUserCommands
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public int AppUserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
