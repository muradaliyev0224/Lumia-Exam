using Lumia.DataContext;
using Lumia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly LumiaDbContext _lumiaDbContext;

        public SettingsController(LumiaDbContext lumiaDbContext)
        {
            _lumiaDbContext = lumiaDbContext;
        }

        public IActionResult Index()
        {
            List<Settings> settings = _lumiaDbContext.Settings.ToList();

            return View(settings);
        }

        public IActionResult Edit(int id)
        {
            Settings setting = _lumiaDbContext.Settings.FirstOrDefault(x => x.Id == id);

            return View(setting);
        }

        [HttpPost]
        public IActionResult Edit(Settings changesSettings)
        {
            if(!ModelState.IsValid) return View(changesSettings);

            Settings settings = _lumiaDbContext.Settings.FirstOrDefault(x => x.Id == changesSettings.Id);

            if(settings == null)
            {
                return View(changesSettings);
            }

            settings.Value = changesSettings.Value;

            _lumiaDbContext.SaveChanges();

            return RedirectToAction("Index", "Settings");
        }

    }
}
