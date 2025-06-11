using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Features.Mediator.Results.AppUserResults;
using Application.Interfaces;
using Application.Interfaces.ICurrentUserContext;
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
    public class GetCheckAppUserQueryHandler : IRequestHandler<GetCheckAppUserQuery, GetCheckAppUserQueryResult>
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IRepository<UserLoginHistory> _userLoginHistoryRepository;

        public GetCheckAppUserQueryHandler(UserManager<AppUser> userManager, ICurrentUserContext currentUserContext, IRepository<UserLoginHistory> userLoginHistoryRepository)
        {
            _userManager = userManager;
            _currentUserContext = currentUserContext;
            _userLoginHistoryRepository = userLoginHistoryRepository;
        }

        public async Task<GetCheckAppUserQueryResult> Handle(GetCheckAppUserQuery request, CancellationToken cancellationToken)
        {
            var values = new GetCheckAppUserQueryResult();

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                values.IsExist = false;
            }
            else
            {
                var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

                if (passwordVerificationResult == PasswordVerificationResult.Success)
                    values.IsExist = true;
                values.UserName = user.UserName;
                values.Id = user.Id;
                var userRoles = await _userManager.GetRolesAsync(user);
                values.Roles = userRoles.ToList();

                var loginHistory = new UserLoginHistory
                {
                    UserId = user?.Id.ToString(),
                    UserName = user?.UserName,
                    LoginTime = DateTime.UtcNow,
                    IpAddress = _currentUserContext.IpAddress,
                    UserAgent = _currentUserContext.UserAgent
                };

                await _userLoginHistoryRepository.CreateAsync(loginHistory);
                return values;

            }
            return values;
        }
    }
}
