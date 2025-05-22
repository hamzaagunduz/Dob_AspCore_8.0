using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.AppUserResults
{
    public class GetHealtByUserIdQueryResult
    {
        public int Lives { get; set; } 
        public DateTime? LastLifeAddedTime { get; set; }
    }
}
