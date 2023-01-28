using Lumia.DataContext;
using Lumia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        private readonly LumiaDbContext _lumiaDbContext;

        public PositionController(LumiaDbContext lumiaDbContext)
        {
            _lumiaDbContext = lumiaDbContext;
        }

        public IActionResult Index()
        {
            List<Position> positions = _lumiaDbContext.Positions.ToList();

            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Position newPosition)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Name is required!");
                return View(newPosition);
            }

            _lumiaDbContext.Positions.Add(newPosition);
            _lumiaDbContext.SaveChanges();

            return RedirectToAction("index", "position");
        }

        public IActionResult Edit(int id)
        {
            Position position = _lumiaDbContext.Positions.FirstOrDefault(x => x.Id == id);

            return View(position);
        }

        [HttpPost]
        public IActionResult Edit(Position changedPosition)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Name is required!");
                return View(changedPosition);
            }

            Position position = _lumiaDbContext.Positions.FirstOrDefault(x => x.Id == changedPosition.Id);

            if(position == null) return View(changedPosition);

            position.Name = changedPosition.Name;

            _lumiaDbContext.SaveChanges();

            return RedirectToAction("index", "position");
        }

        public IActionResult Delete(int id)
        {
            Position deletedPosition  = _lumiaDbContext.Positions.FirstOrDefault(x => x.Id == id);

            if (deletedPosition == null) return View(deletedPosition);

            _lumiaDbContext.Positions.Remove(deletedPosition);
            _lumiaDbContext.SaveChanges();
            
            return RedirectToAction("index", "position");
        }

    }
}
