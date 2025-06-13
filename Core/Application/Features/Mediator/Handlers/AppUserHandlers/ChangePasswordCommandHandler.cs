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
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;

        public ChangePasswordCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.AppUserId.ToString());

            if (user == null)
                return false;

            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            return result.Succeeded;
        }
    }
}
