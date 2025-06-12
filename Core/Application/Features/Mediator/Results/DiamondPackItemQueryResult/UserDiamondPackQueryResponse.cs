using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.DiamondPackItemQueryResult
{
    public class UserDiamondPackQueryResponse
    {
        public int DiamondCount { get; set; }
        public List<DiamondPackItemQueryResult> Items { get; set; }
    }

}
