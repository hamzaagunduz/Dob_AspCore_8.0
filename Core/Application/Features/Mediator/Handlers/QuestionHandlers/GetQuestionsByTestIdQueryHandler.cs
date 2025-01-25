using Application.Features.Mediator.Results.QuestionResults;
using Application.Interfaces.IQuestionRepository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetQuestionsByTestIdQueryHandler : IRequestHandler<GetQuestionsByTestIdQuery, List<GetQuestionsByTestIdQueryResult>>
{
    private readonly IQuestionRepository _questionRepository;

    public GetQuestionsByTestIdQueryHandler(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<List<GetQuestionsByTestIdQueryResult>> Handle(GetQuestionsByTestIdQuery request, CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetQuestionsByTestIdAsync(request.TestID);

        // Data transfer object dönüşümü
        return questions.Select(q => new GetQuestionsByTestIdQueryResult
        {
            QuestionID = q.QuestionID,
            Text = q.Text,
            OptionA = q.OptionA,
            OptionB = q.OptionB,
            OptionC = q.OptionC,
            OptionD = q.OptionD,
            OptionE = q.OptionE,
            Answer = q.Answer
        }).ToList();
    }
}
