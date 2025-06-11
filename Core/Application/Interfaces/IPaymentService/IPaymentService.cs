using Application.Features.Mediator.Results.PaymentResults;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IPaymentService
{
    public interface IPaymentService
    {
        Task<CreatePaymentResult> CreatePaymentAsync(string cardHolderName,
            string cardNumber,
            string expireMonth,
            string expireYear,
            string cvc);
        Task HandleCallbackAsync(PaymentCallback callbackData);
    }
}
