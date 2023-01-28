using Lumia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser
            {
                Fullname = "Murad Aliyev",
                UserName = "muradali",

            };

            var result = await _userManager.CreateAsync(admin, "Murad123");

            return Ok(result);
        }

        public async Task<IActionResult> CreateRoles()
        {
            IdentityRole admin = new IdentityRole("Admin");
            IdentityRole member = new IdentityRole("Member");

            await _roleManager.CreateAsync(admin);
            await _roleManager.CreateAsync(member);

            return Ok("Roles was created");
        }

        public async Task<IActionResult> SetRoleToAdmin()
        {
            AppUser admin = await _userManager.FindByNameAsync("muradali");

            var result = await _userManager.AddToRoleAsync(admin, "Admin");

            return Ok(result);
        }

    }
}
