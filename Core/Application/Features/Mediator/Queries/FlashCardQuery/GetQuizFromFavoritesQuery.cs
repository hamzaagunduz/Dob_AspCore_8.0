using Application.Features.Mediator.Results.FlashCardResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.FlashCardQuery
{
    // GetQuizFromFavoritesQuery.cs
    public class GetQuizFromFavoritesQuery : IRequest<GetQuizFromFavoritesQueryResult>
    {
        public int AppUserId { get; set; }
        public int CourseId { get; set; }
        public int QuestionCount { get; set; } = 10;
    }
}
