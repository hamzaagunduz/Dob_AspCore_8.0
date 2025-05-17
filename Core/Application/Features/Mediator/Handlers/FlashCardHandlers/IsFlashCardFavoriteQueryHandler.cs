using Application.Features.Mediator.Queries.FlashCardQuery;
using Application.Interfaces.IFlashCardRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.FlashCardHandlers
{
    public class IsFlashCardFavoriteQueryHandler : IRequestHandler<IsFlashCardFavoriteQuery, bool>
    {
        private readonly IFlashCardRepository _repository;

        public IsFlashCardFavoriteQueryHandler(IFlashCardRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(IsFlashCardFavoriteQuery request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetUserFlashCardAsync(request.AppUserID, request.FlashCardID);
            return existing != null;
        }
    }

}
