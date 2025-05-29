using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Interfaces.IUserRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class PurchaseDiamondCommandHandler : IRequestHandler<PurchaseDiamondCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<PurchaseDiamondCommandHandler> _logger;

        public PurchaseDiamondCommandHandler(IUserRepository userRepository, ILogger<PurchaseDiamondCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(PurchaseDiamondCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogWarning("User with id {UserId} not found.", request.UserId);
                return false;
            }

            // Bu noktada ödeme işlemi gerçekleştirilmelidir (3rd party payment entegrasyonu simüle edilir)
            _logger.LogInformation("Processing payment for user {UserId}, amount: {Amount}", request.UserId, request.Amount);
            bool paymentSuccess = true; // Simülasyon, gerçekte ödeme API'si çağrılmalı

            if (!paymentSuccess)
            {
                _logger.LogWarning("Payment failed for user {UserId}.", request.UserId);
                return false;
            }

            user.Diamond += request.DiamondCount;
            await _userRepository.UpdateAsync(user);

            _logger.LogInformation("User {UserId} purchased {DiamondCount} diamonds.", user.Id, request.DiamondCount);

            return true;
        }
    }
}
