using Application.Features.Mediator.Commands.PaymentCommands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Pay([FromBody] CreatePaymentCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(new
            {
                Content = result.HtmlContent,
                ConversationId = result.ConversationId
            });
        }


        [HttpPost]
        public async Task<IActionResult> PayCallBack([FromForm] IFormCollection form)
        {
            foreach (var key in form.Keys)
            {
                var value = form[key];
                Console.WriteLine($"{key}: {value}");
            }
            var callbackData = new PaymentCallback
            {
                Status = form["status"],
                PaymentId = form["paymentId"],
                ConversationData = form["conversationData"],
                ConversationId = form["conversationId"],
                MDStatus = form["mdStatus"]
            };

            await _mediator.Send(new HandlePaymentCallbackCommand(callbackData));
            return Ok();
        }
    }
}
