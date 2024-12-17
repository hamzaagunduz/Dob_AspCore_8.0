using Application.Features.Mediator.Queries.AppUserQueries;
using Application.Features.Mediator.Results.AppUserResults;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class GetAllAppUserQueryHandler : IRequestHandler<GetAllAppUserQuery, List<GetAllAppUserQueryResult>>
    {
        private readonly IRepository<AppUser> _repository;

        public GetAllAppUserQueryHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetAllAppUserQueryResult>> Handle(GetAllAppUserQuery request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetAllAsync();

            return values.Select(x => new GetAllAppUserQueryResult
            {
                UserId = x.Id,
                Email = x.Email,
                SurName = x.SurName,
                ExamID = x.ExamID,
                FirstName = x.FirstName,
                ImageURL = x.ImageURL
            }).ToList();
        }
    }
}
