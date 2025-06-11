using Application.Features.Mediator.Commands.PaymentCommands;
using Application.Features.Mediator.Results.PaymentResults;
using Application.Interfaces.IPaymentService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.PaymentHandlers
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentResult>
    {
        private readonly IPaymentService _paymentService;

        public CreatePaymentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<CreatePaymentResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            return await _paymentService.CreatePaymentAsync(
                  request.CardHolderName,
                  request.CardNumber,
                  request.ExpireMonth,
                  request.ExpireYear,
                  request.Cvc
              );
        }
    }
}
