using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserCustomFlashCard
    {
        public int UserCustomFlashCardID { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }
        public int CourseID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
