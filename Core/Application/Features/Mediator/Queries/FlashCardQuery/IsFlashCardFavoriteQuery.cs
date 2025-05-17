using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.FlashCardQuery
{
    public class IsFlashCardFavoriteQuery : IRequest<bool>
    {
        public int AppUserID { get; set; }
        public int FlashCardID { get; set; }

        public IsFlashCardFavoriteQuery(int appUserID, int flashCardID)
        {
            AppUserID = appUserID;
            FlashCardID = flashCardID;
        }
    }

}
