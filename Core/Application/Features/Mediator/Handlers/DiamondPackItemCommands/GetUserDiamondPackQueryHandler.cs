using Application.Features.Mediator.Queries.DiamondPackItemQuery;
using Application.Features.Mediator.Results.DiamondPackItemQueryResult;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.DiamondPackItemCommands
{
    public class GetUserDiamondPackQueryHandler : IRequestHandler<GetUserDiamondPackQuery, UserDiamondPackQueryResponse>
    {
        private readonly IRepository<DiamondPackItem> _diamondPackRepository;
        private readonly IRepository<AppUser> _userRepository;

        public GetUserDiamondPackQueryHandler(
            IRepository<DiamondPackItem> diamondPackRepository,
            IRepository<AppUser> userRepository)
        {
            _diamondPackRepository = diamondPackRepository;
            _userRepository = userRepository;
        }

        public async Task<UserDiamondPackQueryResponse> Handle(GetUserDiamondPackQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            var items = await _diamondPackRepository.GetAllAsync();

            var itemResults = items.Select(value => new DiamondPackItemQueryResult
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Description,
                DiamondAmount = value.DiamondAmount,
                BonusPercentage = value.BonusPercentage,
                PriceInTL = value.PriceInTL,
                ImageUrl = value.ImageUrl
            }).ToList();

            return new UserDiamondPackQueryResponse
            {
                DiamondCount = user?.Diamond ?? 0,
                Items = itemResults
            };
        }
    }

}
