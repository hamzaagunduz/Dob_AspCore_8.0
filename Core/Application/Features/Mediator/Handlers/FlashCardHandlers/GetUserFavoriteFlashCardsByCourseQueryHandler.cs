using Application.Features.Mediator.Results.FlashCardResults;
using Application.Interfaces.IFlashCardRepository;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetUserFavoriteFlashCardsByCourseQueryHandler : IRequestHandler<GetUserFavoriteFlashCardsByCourseQuery, List<GetUserFavoriteFlashCardsByCourseResult>>
{
    private readonly IFlashCardRepository _repository;

    public GetUserFavoriteFlashCardsByCourseQueryHandler(IFlashCardRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetUserFavoriteFlashCardsByCourseResult>> Handle(GetUserFavoriteFlashCardsByCourseQuery request, CancellationToken cancellationToken)
    {
        var flashCards = await _repository.GetUserFavoriteFlashCardsByCourseAsync(request.AppUserID, request.CourseID);

        return flashCards.Select(f => new GetUserFavoriteFlashCardsByCourseResult
        {
            FlashCardID = f.FlashCardID,
            Front = f.Front,
            Back = f.Back,
            QuestionID = f.QuestionID
        }).ToList();
    }
}
