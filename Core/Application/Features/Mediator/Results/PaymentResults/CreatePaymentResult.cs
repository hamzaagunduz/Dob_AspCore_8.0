﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.PaymentResults
{
    public sealed record CreatePaymentResult(
         string HtmlContent,
         string ConversationId,
         string errorMessage
     );
}
