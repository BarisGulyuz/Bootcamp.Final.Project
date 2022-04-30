using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teleperformance.UI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*Kullanıcı Adı Alanı Gerekli")]
        public string UserName { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessage = "*Mail Alanı Gerekli")]
        public string Email { get; set; }
        //public IFormFile Picture { get; set; }
        [Required(ErrorMessage = "*Şifre Alanı Gerekli")]
        public string Password { get; set; }
        [Required(ErrorMessage = "*Şifre Tekrar Gerekli")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler Uyuşmuyor")]
        public string PasswordAgain { get; set; }
    }
}
