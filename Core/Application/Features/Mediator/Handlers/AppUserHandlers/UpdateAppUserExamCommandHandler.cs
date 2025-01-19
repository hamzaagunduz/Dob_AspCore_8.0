using Application.Features.Mediator.Commands.AppUserCommands;
using Application.Interfaces;
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
    public class UpdateAppUserExamCommandHandler : IRequestHandler<UpdateAppUserExamCommand>
    {
        private readonly IRepository<AppUser> _repository;

        public UpdateAppUserExamCommandHandler(IRepository<AppUser> repository, UserManager<AppUser> userManager)
        {
            _repository = repository;

        }

        public async Task Handle(UpdateAppUserExamCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.UserId);
            values.ExamID = request.ExamID;

            await _repository.UpdateAsync(values);
        }
    }
}
