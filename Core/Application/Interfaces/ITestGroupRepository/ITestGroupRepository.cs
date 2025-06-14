using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ITestGroupRepository
{
    public interface ITestGroupRepository
    {
        Task<TestGroup> AddAsync(TestGroup testGroup);

        Task<Test?> GetTestWithGroupAndTopicAsync(int testId);

        Task<int> GetMaxOrderByTestGroupIdAsync(int testGroupId);

        Task<int> GetMaxOrderByTopicIdAsync(int? topicId);


    }
}
