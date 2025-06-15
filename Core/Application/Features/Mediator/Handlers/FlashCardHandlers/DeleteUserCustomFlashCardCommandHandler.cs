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
    public class DeleteUserCustomFlashCardCommandHandler : IRequestHandler<DeleteUserCustomFlashCardCommand>
    {
        private readonly IFlashCardRepository _repository;

        public DeleteUserCustomFlashCardCommandHandler(IFlashCardRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteUserCustomFlashCardCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetUserCustomFlashCardByIdAsync(request.Id);

            if (entity != null)
            {
                await _repository.DeleteUserCustomFlashCardAsync(entity);
            }
        }
    }
}
