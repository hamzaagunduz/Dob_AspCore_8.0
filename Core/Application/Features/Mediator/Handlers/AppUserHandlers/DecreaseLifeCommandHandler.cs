using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Interfaces.IShopRepository;
using Application.Interfaces.IUserRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class DecreaseLifeCommandHandler : IRequestHandler<DecreaseLifeCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DecreaseLifeCommandHandler> _logger;
        private readonly IShopRepository _shopRepository;

        public DecreaseLifeCommandHandler(IUserRepository userRepository, ILogger<DecreaseLifeCommandHandler> logger, IShopRepository shopRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _shopRepository = shopRepository;
        }

        public async Task<bool> Handle(DecreaseLifeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning("User with id {UserId} not found.", request.UserId);
                return false;
            }

            if (user.Lives <= 0)
            {
                _logger.LogInformation("User with id {UserId} has no lives left.", request.UserId);
                return false;
            }
            bool premium = await _shopRepository.HasActiveShopItemAsync(request.UserId, 2);
            if (premium)
            {
                _logger.LogInformation("User with id {UserId} is premium. Life is not decreased.", request.UserId);
                return true;
            }
            user.Lives -= 1;

            await _userRepository.UpdateAsync(user);

            _logger.LogInformation("User with id {UserId} lost a life. Remaining lives: {Lives}", user.Id, user.Lives);

            return true;
        }
    }
}
