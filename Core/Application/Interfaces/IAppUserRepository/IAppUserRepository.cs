﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IAppUserRepository
{
    public interface IAppUserRepository
    {
        Task<int> GetTotalUserCountAsync();
        Task<int> GetTotalDiamondsAsync();
        Task<List<AppUserInfoDto>> GetLastFiveUsersAsync();
        Task<(List<AppUser> Users, int TotalCount)> GetPagedUsersWithStatisticsAsync(
    int pageNumber,
    int pageSize);

    }


    public class AppUserInfoDto
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
    }
}
