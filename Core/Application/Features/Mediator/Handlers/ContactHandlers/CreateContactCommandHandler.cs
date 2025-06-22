using Application.Features.Mediator.Commands.ContactCommands;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ContactHandlers
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, int>
    {
        private readonly IRepository<Contact> _repository;

        public CreateContactCommandHandler(IRepository<Contact> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var entity = new Contact
            {
                Name = request.Name,
                Email = request.Email,
                Subject = request.Subject,
                Message = request.Message
            };

            await _repository.CreateAsync(entity);
            return entity.Id;
        }
    }
}
