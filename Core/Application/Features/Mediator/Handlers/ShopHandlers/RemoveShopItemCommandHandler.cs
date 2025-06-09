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
    public class RemoveShopItemCommandHandler : IRequestHandler<RemoveShopItemCommand>
    {
        private readonly IRepository<ShopItem> _repository;

        public RemoveShopItemCommandHandler(IRepository<ShopItem> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveShopItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(request.Id);
            if (item != null)
            {
                await _repository.RemoveAsync(item);
            }
        }
    }
}
