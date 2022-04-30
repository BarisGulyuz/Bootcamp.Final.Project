using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Teleperformance.Core.Helpers;
using Teleperformance.Core.Helpers.FileUpload;
using Teleperformance.Entities.Concrete;
using Teleperformance.UI.Models;

namespace Teleperformance.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            //string imageUrl = "";
            //CloudinaryHelper cloudinaryHelper = new CloudinaryHelper();
            //if (register.Picture is not null) imageUrl = cloudinaryHelper.AddPhotoAndGetUrl(register.Picture);
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = register.UserName,
                    FullName = register.FullName,
                    Email = register.Email,
                };
                IdentityResult ıdentityResult = await _userManager.CreateAsync(user, register.Password);
                if (ıdentityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Login");
                }
                foreach (var error in ıdentityResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(register);
            }
            return View(register);

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                login.RememberMe = true;
                User user = await _userManager.FindByEmailAsync(login.Email);
                if (user is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlış, tekrar deneyiniz");
                        return View(login);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-posta adresiniz veya şifreniz yanlış, , tekrar deneyiniz");
                    return View(login);
                }
            }
            return View(login);

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetRandomPassword()
        {
            string password = PasswordHelper.GenerateRandomPassword(PasswordHelper.PasswordSize.Strong);
            return Json(password);
        }

        //[HttpGet]
        //public async Task<IActionResult> ChangePassword()
        //{
        //    var user = await _userManager.FindByEmailAsync("bar.77@windowslive.com");
        //    await _userManager.ChangePasswordAsync(user, "xxxxxxxxxx", "Asdf1234");
        //    return View();
        //}

    }
}

