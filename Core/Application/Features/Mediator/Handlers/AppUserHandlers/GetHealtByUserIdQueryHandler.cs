using MediatR;
using Application.Interfaces.IUserRepository;
using Application.Features.Mediator.Results.AppUserResults;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Interfaces.IShopRepository;

public class GetHealtByUserIdQueryHandler : IRequestHandler<GetHealtByUserIdQuery, GetHealtByUserIdQueryResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IShopRepository _shopRepository;

    public GetHealtByUserIdQueryHandler(IUserRepository userRepository, IShopRepository repository)
    {
        _userRepository = userRepository;
        _shopRepository = repository;
    }

    public async Task<GetHealtByUserIdQueryResult> Handle(GetHealtByUserIdQuery request, CancellationToken cancellationToken)
    {
        var (lives, lastLifeAddedTime) = await _userRepository.GetLivesAndLastLifeAddedTimeAsync(request.id);

        bool premium= await _shopRepository.HasActiveShopItemAsync(request.id,2);

        return new GetHealtByUserIdQueryResult
        {
            Lives = premium ? 999 : lives,
            LastLifeAddedTime = lastLifeAddedTime
        };
    }
}
