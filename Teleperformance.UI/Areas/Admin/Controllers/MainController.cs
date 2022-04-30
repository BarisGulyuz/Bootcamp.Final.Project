using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Teleperformance.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MainController : Controller
    {
        public async Task<IActionResult> Index()
        {
            bool userAuthenticate = User.Identity.IsAuthenticated;
            string userName = User.Identity.Name;
            bool isInAdminRole = User.IsInRole("Member");
            bool userHasClaim = User.HasClaim(x => x.Type == ClaimTypes.NameIdentifier);

            List<string> myRoles = new List<string>
            {
                "Admin",
                "Member",
                "Customer",
                "Edıtor"
            };

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "1"));
            claims.Add(new Claim(ClaimTypes.Email, "x@hotmail.com"));
            foreach (string role in myRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.Add(new Claim("Soyad", "Gülyüz"));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, authenticationType: CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            AuthenticationProperties authenticationProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }
    }
}
