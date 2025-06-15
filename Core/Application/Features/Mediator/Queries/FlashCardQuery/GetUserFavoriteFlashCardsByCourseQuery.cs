using Application.Features.Mediator.Results.FlashCardResults;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;

public class GetUserFavoriteFlashCardsByCourseQuery : IRequest<PaginatedResult<CombinedFlashCardResult>>
{
    public int AppUserID { get; }
    public int CourseID { get; }
    public int PageNumber { get; }
    public int PageSize { get; }

    public GetUserFavoriteFlashCardsByCourseQuery(int appUserID, int courseID, int pageNumber = 1, int pageSize = 10)
    {
        AppUserID = appUserID;
        CourseID = courseID;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
