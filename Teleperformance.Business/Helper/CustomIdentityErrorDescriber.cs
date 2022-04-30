using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.Business.Helper
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = "DuplicateUserName", Description = $"\"{ userName }\" kullanıcı adı kullanılmaktadır." };
        public override IdentityError InvalidUserName(string userName) => new IdentityError { Code = "InvalidUserName", Description = "Geçersiz kullanıcı adı." };
        public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = "DuplicateEmail", Description = $"\"{ email }\" başka bir kullanıcı tarafından kullanılmaktadır." };
        public override IdentityError InvalidEmail(string email) => new IdentityError { Code = "InvalidEmail", Description = "Geçersiz email." };
        public override IdentityError PasswordRequiresLower() => new IdentityError { Code = "PasswordRequiresLower", Description = "Şifre en az 1 küçük harf içermeli" };
        public override IdentityError PasswordRequiresUpper() => new IdentityError { Code = "PasswordRequiresUpper", Description = "Şifre en az 1 büyük harf içermeli" };
        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = "PasswordRequiresDigit", Description = "Şifre en az 1 rakam içermeli" };

    }
}
