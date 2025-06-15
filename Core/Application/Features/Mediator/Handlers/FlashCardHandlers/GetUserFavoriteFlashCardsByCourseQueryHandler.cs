using Application.Features.Mediator.Results.FlashCardResults;
using Application.Interfaces.IFlashCardRepository;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetUserFavoriteFlashCardsByCourseQueryHandler : IRequestHandler<GetUserFavoriteFlashCardsByCourseQuery, PaginatedResult<CombinedFlashCardResult>>
{
    private readonly IFlashCardRepository _repository;

    public GetUserFavoriteFlashCardsByCourseQueryHandler(IFlashCardRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<CombinedFlashCardResult>> Handle(GetUserFavoriteFlashCardsByCourseQuery request, CancellationToken cancellationToken)
    {
        // Sistem flash kartları
        var systemFlashCards = await _repository.GetUserFavoriteFlashCardsByCourseAsync(request.AppUserID, request.CourseID);
        var systemResults = systemFlashCards.Select(f => new CombinedFlashCardResult
        {
            FlashCardID = f.FlashCardID,
            Front = f.Front,
            Back = f.Back,
            QuestionID = f.QuestionID,
            Type = "System"
        }).ToList();

        // Özel flash kartlar
        var customFlashCards = await _repository.GetUserCustomFlashCardsByCourseAsync(request.AppUserID, request.CourseID);
        var customResults = customFlashCards.Select(c => new CombinedFlashCardResult
        {
            UserCustomFlashCardID = c.UserCustomFlashCardID,
            Front = c.Front,
            Back = c.Back,
            Type = "Custom"
        }).ToList();

        // Birleştir
        var combinedResults = systemResults.Concat(customResults).ToList();
        var totalCount = combinedResults.Count;

        // Sayfalama
        var items = combinedResults
            .OrderBy(x => x.Type) // Önce sistem sonra özel
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        return new PaginatedResult<CombinedFlashCardResult>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
