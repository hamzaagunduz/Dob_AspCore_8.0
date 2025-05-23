﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.AppUserCommands
{
    public class DecreaseLifeCommand : IRequest<bool>
    {
        public int UserId { get; }

        public DecreaseLifeCommand(int userId)
        {
            UserId = userId;
        }
    }
}
