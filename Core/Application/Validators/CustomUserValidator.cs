using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();

            // Kullanıcı adı boş mu?
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add(new IdentityError
                {
                    Code = "UserNameRequired",
                    Description = "Kullanıcı adı zorunludur."
                });
            }

            // E-posta geçerli mi?
            if (!string.IsNullOrEmpty(user.Email) && !user.Email.Contains("@"))
            {
                errors.Add(new IdentityError
                {
                    Code = "InvalidEmail",
                    Description = "Geçerli bir e-posta adresi giriniz."
                });
            }

            // Kullanıcı adı daha önce alınmış mı?
            var existingUserByName = await manager.FindByNameAsync(user.UserName);
            if (existingUserByName != null && existingUserByName.Id != user.Id)
            {
                errors.Add(new IdentityError
                {
                    Code = "DuplicateUserName",
                    Description = "Bu kullanıcı adı zaten alınmış."
                });
            }

            // E-posta daha önce alınmış mı?
            if (!string.IsNullOrEmpty(user.Email))
            {
                var existingUserByEmail = await manager.FindByEmailAsync(user.Email);
                if (existingUserByEmail != null && existingUserByEmail.Id != user.Id)
                {
                    errors.Add(new IdentityError
                    {
                        Code = "DuplicateEmail",
                        Description = "Bu e-posta adresi zaten kullanılıyor."
                    });
                }
            }

            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }
    }
}
