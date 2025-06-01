using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Interfaces.IUserDailyMissionRepository;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, CommandResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserDailyMissionRepository _userDailyMissionRepository;

        public CreateAppUserCommandHandler(UserManager<AppUser> userManager, IUserDailyMissionRepository userDailyMissionRepository)
        {
            _userManager = userManager;
            _userDailyMissionRepository = userDailyMissionRepository;
        }

        public async Task<CommandResult> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new AppUser
            {
                FirstName = request.FirstName,
                UserName = request.UserName,
                SurName = request.SurName,
                Email = request.Email,
                ImageURL = request.ImageURL,
                ExamID = request.ExamID,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                // Hata mesajlarını debug loguna yaz
                Debug.WriteLine("Kullanıcı oluşturulurken hata oluştu:");
                foreach (var error in result.Errors)
                {
                    Debug.WriteLine($"Kod: {error.Code}, Açıklama: {error.Description}");
                }

                // Hataları dön
                var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}");
                return new CommandResult
                {
                    Success = false,
                    Errors = errors
                };
            }
            await _userManager.AddToRoleAsync(newUser, "Student");


            int userId = newUser.Id;
            await _userDailyMissionRepository.CreateUserDailyMissionsForUserAsync(userId);

            return new CommandResult
            {
                Success = true
            };
        }
    }
}
