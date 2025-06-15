using Application.Features.Mediator.Results.FlashCardResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.FlashCardQuery
{
    public record GetUserCustomFlashCardsQuery(int AppUserId) : IRequest<List<GetUserCustomFlashCardsQueryResult>>;
}
