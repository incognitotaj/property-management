using Admin.ViewModels;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // To protect from CSRF
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    userName: model.Username,
                    password: model.Password,
                    isPersistent: model.RememberMe,
                    lockoutOnFailure: false);


                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", new { area = "", controller = "Home" });
                }
            }

            ModelState.AddModelError("", "Username / password is invalid");

            return View(model);
        }
        #endregion

        #region Access Denied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion

        #region Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", new { controller = "Auth" });
        }
        #endregion

        #region Forgot password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        //TODO: Send Email to user
                    }

                    ModelState.AddModelError("", "User not found!");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured: {ex.Message}");
                }
            }
            else
            {
                ModelState.AddModelError("", "Provide all required and valid data to proceed");
            }
            return View(model);
        }
        #endregion

        #region Change Password
        [HttpGet]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();
            return View(model);
        }
        #endregion
    }
}
