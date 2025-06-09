using Application.Features.Mediator.Commands.ShopCommands;
using Application.Interfaces;
using Application.Interfaces.IShopRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ShopHandlers
{
    public class UpdateShopItemCommandHandler : IRequestHandler<UpdateShopItemCommand>
    {
        private readonly IRepository<ShopItem> _repository;

        public UpdateShopItemCommandHandler(IRepository<ShopItem> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateShopItemCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _repository.GetByIdAsync(request.Id);
            if (existingItem != null)
            {
                existingItem.Name = request.Name;
                existingItem.Description = request.Description;
                existingItem.Price = request.Price;
                existingItem.Color = request.Color;
                existingItem.ImageUrl = request.ImageUrl;

                await _repository.UpdateAsync(existingItem);
            }
        }
    }
}
