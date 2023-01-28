using Lumia.DataContext;
using Lumia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lumia.Controllers
{
    public class HomeController : Controller
    {
        private readonly LumiaDbContext _lumiaDbContext;

        public HomeController(LumiaDbContext lumiaDbContext)
        {
            _lumiaDbContext = lumiaDbContext;
        }

        public IActionResult Index()
        {
            return View(_lumiaDbContext.TeamMembers.Include(x => x.Position).ToList());
        }
    }
}