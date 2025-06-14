using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TestGroup
    {
        public int TestGroupID { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }

        // Nullable Topic ilişkisi (eski veriler için gerekli)
        public int? TopicID { get; set; }
        public Topic? Topic { get; set; }
        public int? Order { get; set; }
        public ICollection<Test>? Tests { get; set; } = new List<Test>();
    }



}
