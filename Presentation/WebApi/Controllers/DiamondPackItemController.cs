﻿using Application.Features.Mediator.Commands.DiamondPackItemCommands;
using Application.Features.Mediator.Queries.DiamondPackItemQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiamondPackItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiamondPackItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetDiamondPackItemQuery();
            var values = await _mediator.Send(query);
            return Ok(values);
        }
        [HttpGet("diamond-packs")]
        public async Task<IActionResult> GetUserDiamondPacks()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Geçerli kullanıcı kimliği bulunamadı.");

            var query = new GetUserDiamondPackQuery
            {
                UserId = userId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetDiamondPackItemByIdQuery(id);
            var value = await _mediator.Send(query);
            return value != null ? Ok(value) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDiamondPackItemCommand command)
        {
            await _mediator.Send(command);
            return Ok("Diamond paket öğesi başarıyla eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDiamondPackItemCommand command)
        {
            await _mediator.Send(command);
            return Ok("Diamond paket öğesi başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _mediator.Send(new RemoveDiamondPackItemCommand(id));
            return Ok("Diamond paket öğesi başarıyla silindi");
        }
    }
}
