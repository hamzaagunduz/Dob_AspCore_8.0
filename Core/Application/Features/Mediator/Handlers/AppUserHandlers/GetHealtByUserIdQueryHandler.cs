using MediatR;
using Application.Interfaces.IUserRepository;
using Application.Features.Mediator.Results.AppUserResults;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.AppUserQueries;

public class GetHealtByUserIdQueryHandler : IRequestHandler<GetHealtByUserIdQuery, GetHealtByUserIdQueryResult>
{
    private readonly IUserRepository _userRepository;

    public GetHealtByUserIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetHealtByUserIdQueryResult> Handle(GetHealtByUserIdQuery request, CancellationToken cancellationToken)
    {
        var (lives, lastLifeAddedTime) = await _userRepository.GetLivesAndLastLifeAddedTimeAsync(request.id);

        return new GetHealtByUserIdQueryResult
        {
            Lives = lives,
            LastLifeAddedTime = lastLifeAddedTime
        };
    }
}
