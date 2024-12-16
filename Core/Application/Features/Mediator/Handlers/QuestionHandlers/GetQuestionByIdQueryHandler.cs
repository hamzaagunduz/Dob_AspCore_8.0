using Application.Interfaces;
using Application.Features.Mediator.Results.QuestionResults;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.QuestionQueries;

namespace Application.Features.Mediator.Handlers.QuestionHandlers
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, GetQuestionByIdQueryResult>
    {
        private readonly IRepository<Question> _repository;

        public GetQuestionByIdQueryHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task<GetQuestionByIdQueryResult> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = await _repository.GetByIdAsync(request.QuestionID);
            if (question != null)
            {
                return new GetQuestionByIdQueryResult
                {
                    QuestionID = question.QuestionID,
                    Text = question.Text,
                    Answer = question.Answer,
                    OptionA = question.OptionA,
                    OptionB = question.OptionB,
                    OptionC = question.OptionC,
                    OptionD = question.OptionD,
                    OptionE = question.OptionE,
                    TestID = question.TestID
                };
            }
            return null;
        }
    }
}
