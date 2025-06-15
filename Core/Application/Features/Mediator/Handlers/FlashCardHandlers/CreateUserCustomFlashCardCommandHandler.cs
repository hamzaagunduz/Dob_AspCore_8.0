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
    public class CreateUserCustomFlashCardCommandHandler : IRequestHandler<CreateUserCustomFlashCardCommand>
    {
        private readonly IFlashCardRepository _repository;

        public CreateUserCustomFlashCardCommandHandler(IFlashCardRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateUserCustomFlashCardCommand request, CancellationToken cancellationToken)
        {
            var entity = new UserCustomFlashCard
            {
                Front = request.Front,
                Back = request.Back,
                AppUserID = request.AppUserID,
                CourseID = request.CourseID
            };

            await _repository.CreateUserCustomFlashCardAsync(entity);
        }
    }
}
