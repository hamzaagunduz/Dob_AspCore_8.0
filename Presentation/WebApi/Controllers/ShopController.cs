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

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseShopItem([FromBody] PurchaseShopItemCommand command)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized(new { error = "Geçersiz token veya kullanıcı bulunamadı." });

            command.UserId = userId; // Token'dan gelen ID ile üzerine yaz

            var result = await _mediator.Send(command);

            if (result == "Success")
                return Ok(new { message = "Purchase successful." });

            return BadRequest(new { error = result });
        }



        [HttpGet("items")]
        public async Task<IActionResult> GetShopItemsWithUserStatus()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid token or user not found.");

            var result = await _mediator.Send(new GetShopItemsWithUserStatusQuery { UserId = userId });
            return Ok(result);
        }


    }
}
