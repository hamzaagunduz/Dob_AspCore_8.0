using Application.Features.Mediator.Results.FlashCardResults;
using MediatR;

namespace Application.Features.Mediator.Queries.FlashCardQueries
{
    public class GetFlashCardByIdQuery : IRequest<GetFlashCardByIdQueryResult>
    {
        public int FlashCardID { get; set; }

        public GetFlashCardByIdQuery(int flashCardID)
        {
            FlashCardID = flashCardID;
        }
    }
}
