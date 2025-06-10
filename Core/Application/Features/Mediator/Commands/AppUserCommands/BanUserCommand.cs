using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.AppUserCommands
{
    public class BanUserCommand : IRequest<BanUserResponse>
    {
        public int UserId { get; set; }
        public bool BanStatus { get; set; } // True to ban, False to unban
    }

    public class BanUserResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public bool NewBanStatus { get; set; }
    }
}

