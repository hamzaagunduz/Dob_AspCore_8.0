using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Results.AppUserResults
{
    public class GetAppUserByIdQueryResult
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string? ImageURL { get; set; } //Profil Resmi
        public int? ExamID { get; set; } // Hangi Sınava Çalıştığı
    }
}
