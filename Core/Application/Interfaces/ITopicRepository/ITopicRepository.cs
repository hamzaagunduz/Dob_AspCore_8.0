using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITopicRepository
{
    public interface ITopicRepository
    {
        Task<List<Topic>> GetTopicsWithTestsByCourseIdAsync(int examId);

    }
}
