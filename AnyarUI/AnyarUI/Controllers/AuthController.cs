using Anyar.Business.Validations.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Anyar.Core.Entities;
using Anyor.DataAccess.Contexts;
using Anyar.Business.ViewModels.Auth;
using System;
using FluentValidation.Results;

namespace AnyarUI.Controllers
{
    public class AuthController : Controller
    {

        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;
        public readonly IValidator<LoginViewModel> _validatorLogin;
        public readonly IValidator<RegisterViewModel> _validatorRegister;


        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IValidator<LoginViewModel> validatorLogin, IValidator<RegisterViewModel> validatorRegister)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _validatorLogin = validatorLogin;
            _validatorRegister = validatorRegister;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerView)
        {
            ValidationResult result = await _validatorRegister.ValidateAsync(registerView);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid) return View(registerView);
            if(registerView is null) return BadRequest();
            AppUser appUser = new()
            {
                Fullname = registerView.Fullname,
                Email = registerView.Email,
                UserName = registerView.Username

            };

           var identityResult= await  _userManager.CreateAsync(appUser, registerView.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginView)
        {
            ValidationResult result = await _validatorLogin.ValidateAsync(loginView);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid) return View(loginView);
            if (loginView is null) return BadRequest();

             var user=await _userManager.FindByEmailAsync(loginView.UsernameOrEmail);
            if(user is null)
            {
                user = await _userManager.FindByNameAsync(loginView.UsernameOrEmail);
                if (user is null)
                {
                    ModelState.AddModelError("", "Username or Password Or Email are invalid");
                    return View(loginView);
                }
            }

           var signInResult= await _signInManager.PasswordSignInAsync(user, loginView.Password, loginView.RememberMe, true);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password Or Email are invalid");
                return View(loginView);
            }
            if(signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "More attempt for login");
                return View(loginView);
            }
            return RedirectToAction("Index", "Home");
        }
          
    }
}

