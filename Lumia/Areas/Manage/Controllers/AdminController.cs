using Lumia.Areas.Manage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel)
        {
            if(!ModelState.IsValid) return View(adminLoginViewModel);

            
           


            return RedirectToAction("Index", "Dashboard");
        }

    }
}
