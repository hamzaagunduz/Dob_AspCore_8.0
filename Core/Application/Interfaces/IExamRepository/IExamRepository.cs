using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IExamRepository
{
    public interface IExamRepository
    {
         Task<Exam> GetByIdAsync(int id);
         Task UpdateAsync(Exam exam);
    }
}
