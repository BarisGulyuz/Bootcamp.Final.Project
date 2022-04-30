using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teleperformance.UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Mail Alanı Gerekli")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Alanı Gerekli")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }
}
