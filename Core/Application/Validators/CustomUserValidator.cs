using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();

            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add(new IdentityError
                {
                    Code = "UserNameRequired",
                    Description = "Kullanıcı adı zorunludur."
                });
            }

            if (!string.IsNullOrEmpty(user.Email) && !user.Email.Contains("@"))
            {
                errors.Add(new IdentityError
                {
                    Code = "InvalidEmail",
                    Description = "Geçerli bir e-posta adresi giriniz."
                });
            }

            return Task.FromResult(errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success);
        }
    }
}
