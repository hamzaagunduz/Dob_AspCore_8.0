using Application.Features.Mediator.Commands.FlashCardCommands;
using Application.Interfaces.IFlashCardRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    public class UpdateUserCustomFlashCardCommandHandler : IRequestHandler<UpdateUserCustomFlashCardCommand>
    {
        private readonly IFlashCardRepository _repository;

        public UpdateUserCustomFlashCardCommandHandler(IFlashCardRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateUserCustomFlashCardCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetUserCustomFlashCardByIdAsync(request.UserCustomFlashCardID);

            if (entity != null)
            {
                entity.Front = request.Front;
                entity.Back = request.Back;
                await _repository.UpdateUserCustomFlashCardAsync(entity);
            }
        }
    }
}
