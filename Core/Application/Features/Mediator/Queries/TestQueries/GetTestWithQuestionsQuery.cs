using Application.Features.Mediator.Results.TestResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Queries.TestQueries
{
    public class GetTestWithQuestionsQuery : IRequest<GetTestWithQuestionsQueryResult>
    {
        public GetTestWithQuestionsQuery() { }

        public GetTestWithQuestionsQuery(int testID, int appUserID)
        {
            TestID = testID;
            AppUserID = appUserID;
        }

        public int TestID { get; set; }
        public int AppUserID { get; set; } // Yeni eklendi


    
    }
}
