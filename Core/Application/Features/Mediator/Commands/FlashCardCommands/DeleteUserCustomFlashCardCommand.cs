using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.FlashCardCommands
{
    public record DeleteUserCustomFlashCardCommand(int Id) : IRequest;

}
