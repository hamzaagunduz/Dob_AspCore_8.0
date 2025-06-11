using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserLoginHistory
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? LoginTime { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }

}
