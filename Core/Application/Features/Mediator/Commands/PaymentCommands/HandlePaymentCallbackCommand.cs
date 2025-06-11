
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.PaymentCommands
{
    public sealed record HandlePaymentCallbackCommand(
        PaymentCallback CallbackData
    ) : IRequest;
}
