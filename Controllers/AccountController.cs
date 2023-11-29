using Blog.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> loginManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> loginManager)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email
                };
                var identityResult = await userManager.CreateAsync(identityUser, registerModel.Password);
                if (identityResult.Succeeded)
                {
                    //asign roles
                    var userRoleAsign = await userManager.AddToRoleAsync(identityUser, "User");
                    if (userRoleAsign.Succeeded)
                    {
                        return RedirectToAction("Register");
                    }
                }
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await loginManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);

                if (loginResult != null && loginResult.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(loginModel.ReturnUrl))
                    {
                        return Redirect(loginModel.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await loginManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult accessDenied()
        {
            return View();
        }

    }
}
