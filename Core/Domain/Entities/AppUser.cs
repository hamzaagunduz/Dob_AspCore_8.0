using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string? ImageURL { get; set; } //Profil Resmi
        public int? ExamID { get; set; } // Hangi Sınava Çalıştığı

        public ICollection<AppUserFlashCard> AppUserFlashCards { get; set; } = new List<AppUserFlashCard>();

    }
}
