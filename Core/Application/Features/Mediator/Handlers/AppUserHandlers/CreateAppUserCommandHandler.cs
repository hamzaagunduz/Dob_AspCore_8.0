using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public CreateAppUserCommandHandler( UserManager<AppUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
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
            }
        }
    }
}