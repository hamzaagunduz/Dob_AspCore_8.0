using Application.Features.Mediator.Commands.ShopCommands;
using Application.Features.Mediator.Queries.ShopQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/shop
        [HttpGet]
        public async Task<IActionResult> GetAllShopItems()
        {
            var result = await _mediator.Send(new GetShopItemsQuery());
            return Ok(result);
        }

        // POST: api/shop
        [HttpPost]
        public async Task<IActionResult> CreateShopItem([FromBody] CreateShopItemCommand command)
        {
            await _mediator.Send(command);
            return Ok(new { message = "Shop item created successfully." });
        }
    }
}
