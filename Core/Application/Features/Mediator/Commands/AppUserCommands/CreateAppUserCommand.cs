using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mediator.Commands.AppUserCommands
{
    public class CreateAppUserCommand : IRequest<CommandResult>
    {
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ImageURL { get; set; } // Profil Resmi
        public int? ExamID { get; set; } // Hangi Sınava Çalıştığı
    }

    // Command sonucu için kullanılacak sınıf
    public class CommandResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
