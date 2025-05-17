using Application.Features.Mediator.Results.FlashCardResults;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;

public class GetUserFavoriteFlashCardsByCourseQuery : IRequest<List<GetUserFavoriteFlashCardsByCourseResult>>
{
    public int AppUserID { get; }
    public int CourseID { get; }

    public GetUserFavoriteFlashCardsByCourseQuery(int appUserID, int courseID)
    {
        AppUserID = appUserID;
        CourseID = courseID;
    }
}
