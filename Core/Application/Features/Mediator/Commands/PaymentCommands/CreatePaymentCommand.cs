using Application.Features.Mediator.Results.PaymentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.PaymentCommands
{
    public sealed record CreatePaymentCommand(
        string CardHolderName,
        string CardNumber,
        string ExpireMonth,
        string ExpireYear,
        string Cvc
    ) : IRequest<CreatePaymentResult>;
}
