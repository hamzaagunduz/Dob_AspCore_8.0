using Application.Interfaces;
using Application.Features.Mediator.Results.QuestionResults;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Mediator.Queries.QuestionQueries;

namespace Application.Features.Mediator.Handlers.QuestionHandlers
{
    public class GetAllQuestionQueryHandler : IRequestHandler<GetAllQuestionQuery, List<GetAllQuestionQueryResult>>
    {
        private readonly IRepository<Question> _repository;

        public GetAllQuestionQueryHandler(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllQuestionQueryResult>> Handle(GetAllQuestionQuery request, CancellationToken cancellationToken)
        {
            var questions = await _repository.GetAllAsync();
            return questions.Select(question => new GetAllQuestionQueryResult
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
            }).ToList();
        }
    }
}
