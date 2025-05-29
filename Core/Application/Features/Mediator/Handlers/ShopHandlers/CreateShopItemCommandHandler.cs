using Application.Features.Mediator.Commands.ShopCommands;
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
    public class CreateShopItemCommandHandler : IRequestHandler<CreateShopItemCommand>
    {
        private readonly IShopRepository _repository;

        public CreateShopItemCommandHandler(IShopRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateShopItemCommand request, CancellationToken cancellationToken)
        {
            var newItem = new ShopItem
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Color = request.Color,
                ImageUrl = request.ImageUrl
            };

            await _repository.AddAsync(newItem);
        }
    }
}
