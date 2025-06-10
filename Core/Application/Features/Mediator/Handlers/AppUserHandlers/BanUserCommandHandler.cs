using Application.Features.Mediator.Commands.AppUserCommands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class BanUserCommandHandler : IRequestHandler<BanUserCommand, BanUserResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public BanUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BanUserResponse> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                return new BanUserResponse
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı",
                    NewBanStatus = false
                };
            }

            // Update ban status
            user.Ban = request.BanStatus;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new BanUserResponse
                {
                    Success = false,
                    Message = "Ban durumu güncellenemedi",
                    NewBanStatus = user.Ban ?? false
                };
            }

            return new BanUserResponse
            {
                Success = true,
                Message = request.BanStatus ?
                    "Kullanıcı başarıyla banlandı" :
                    "Kullanıcı banı başarıyla kaldırıldı",
                NewBanStatus = user.Ban ?? false
            };
        }
    }
}
