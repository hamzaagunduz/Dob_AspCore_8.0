using Application.Features.Mediator.Commands.PaymentCommands;
using Application.Interfaces.IPaymentService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.PaymentHandlers
{
    public class HandlePaymentCallbackHandler : IRequestHandler<HandlePaymentCallbackCommand>
    {
        private readonly IPaymentService _paymentService;

        public HandlePaymentCallbackHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(HandlePaymentCallbackCommand request, CancellationToken cancellationToken)
        {
            await _paymentService.HandleCallbackAsync(request.CallbackData);
        }
    }
}
