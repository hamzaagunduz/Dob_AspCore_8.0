using Application.Features.Mediator.Commands.FlashCardCommands;
using Application.Interfaces.IFlashCardRepository;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    public class AddUserFlashCardCommandHandler : IRequestHandler<AddUserFlashCardCommand>
    {
        private readonly IFlashCardRepository _repository;

        public AddUserFlashCardCommandHandler(IFlashCardRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(AddUserFlashCardCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetUserFlashCardAsync(request.AppUserID, request.FlashCardID);

            if (existing != null)
            {
                // Daha önce eklenmişse => kaldır
                await _repository.RemoveUserFlashCardAsync(existing);
            }
            else
            {
                // Yoksa => ekle
                var entity = new AppUserFlashCard
                {
                    AppUserID = request.AppUserID,
                    FlashCardID = request.FlashCardID
                };

                await _repository.AddAsync(entity);
            }
        }
    }


}
