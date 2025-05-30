﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IUserTopicPerformanceRepository
{
    public interface IUserTopicPerformanceRepository
    {
        Task AddRangeAsync(IEnumerable<UserTopicPerformance> performances);
        Task SaveChangesAsync();
        IQueryable<UserTopicPerformance> GetQueryable();

    }

}
