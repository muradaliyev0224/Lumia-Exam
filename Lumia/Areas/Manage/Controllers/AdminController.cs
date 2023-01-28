using Lumia.Areas.Manage.ViewModels;
using Lumia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Username or password incorrect");
                return View(adminLoginViewModel);
            }

            AppUser admin = await _userManager.FindByNameAsync(adminLoginViewModel.Username);

            if (admin == null)
            {
                ModelState.AddModelError("", "Admin not found");
                return View(adminLoginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginViewModel.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password incorrect");
                return View(adminLoginViewModel);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "home", new { Area = "" });
        }

    }
}
