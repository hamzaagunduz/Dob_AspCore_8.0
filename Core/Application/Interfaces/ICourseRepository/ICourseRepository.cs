using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ICourseRepository
{
    public interface ICourseRepository
    {
        Task<ICollection<Course>> GetCoursesByExamIdAsync(int examId);
        Task<int> GetMaxOrderByExamIdAsync(int examId);

    }
}
