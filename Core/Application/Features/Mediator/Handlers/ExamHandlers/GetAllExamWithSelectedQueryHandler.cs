using Application.Features.Mediator.Queries.ExamQueries;
using Application.Features.Mediator.Results.ExamResults;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.ExamHandlers
{
    public class GetAllExamWithSelectedQueryHandler : IRequestHandler<GetAllExamWithSelectedQuery, List<GetAllExamWithSelectedQueryResult>>
    {
        private readonly IRepository<Exam> _examRepository;
        private readonly IRepository<AppUser> _userRepository;

        public GetAllExamWithSelectedQueryHandler(IRepository<Exam> examRepository, IRepository<AppUser> userRepository)
        {
            _examRepository = examRepository;
            _userRepository = userRepository;
        }

        public async Task<List<GetAllExamWithSelectedQueryResult>> Handle(GetAllExamWithSelectedQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return new List<GetAllExamWithSelectedQueryResult>(); // veya exception

            var selectedExamId = user.ExamID;

            var exams = await _examRepository.GetAllAsync();

            var result = exams.Select(exam => new GetAllExamWithSelectedQueryResult
            {
                ExamID = exam.ExamID,
                Name = exam.Name,
                Year = exam.Year,
                IsSelected = selectedExamId.HasValue && exam.ExamID == selectedExamId.Value
            }).ToList();

            return result;
        }
    }
}
