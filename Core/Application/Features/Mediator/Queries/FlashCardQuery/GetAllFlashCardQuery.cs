using MediatR;
using System.Collections.Generic;

namespace Application.Features.Mediator.Queries.FlashCardQuery
{
    public class GetAllFlashCardQuery : IRequest<List<GetAllFlashCardQueryResult>>
    {
    }
}
