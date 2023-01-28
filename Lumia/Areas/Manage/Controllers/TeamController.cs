using Lumia.DataContext;
using Lumia.Helpers;
using Lumia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Lumia.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class TeamController : Controller
    {
        private readonly LumiaDbContext _lumiaDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamController(LumiaDbContext lumiaDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _lumiaDbContext = lumiaDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int page = 1)
        {
            var query = _lumiaDbContext.TeamMembers.Include(x => x.Position).AsQueryable();

            var paginatedTeamMembers = PaginatedList<TeamMember>.Create(query, 2, page);

            return View(paginatedTeamMembers);
        }

        public IActionResult Create()
        {
            ViewBag.Positions = _lumiaDbContext.Positions.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(TeamMember newTeamMember)
        {
            if(!ModelState.IsValid) return View(newTeamMember);

            if(newTeamMember.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image file is required!");
                return View(newTeamMember);
            }
            else
            {
                newTeamMember.ImageName = FileManager.SaveImage(_webHostEnvironment.WebRootPath, "uploads/teammembers", newTeamMember.ImageFile);
            }

            _lumiaDbContext.TeamMembers.Add(newTeamMember);
            _lumiaDbContext.SaveChanges();

            return RedirectToAction("index", "team");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Positions = _lumiaDbContext.Positions.ToList();

            TeamMember members = _lumiaDbContext.TeamMembers.FirstOrDefault(x => x.Id == id);

            return View(members);
        }

        [HttpPost]
        public IActionResult Edit(TeamMember changedTeamMember)
        {
            ViewBag.Positions = _lumiaDbContext.Positions.ToList();

            TeamMember teamMember = _lumiaDbContext.TeamMembers.FirstOrDefault(x => x.Id == changedTeamMember.Id);

            if (teamMember == null) return View(teamMember);

            if(changedTeamMember.ImageFile != null)
            {
                string oldImageName = teamMember.ImageName;
                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/teammembers", oldImageName);

                if(System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                teamMember.ImageName = FileManager.SaveImage(_webHostEnvironment.WebRootPath, "uploads/teammembers", changedTeamMember.ImageFile);
            }

            teamMember.Name = changedTeamMember.Name;
            teamMember.Surname = changedTeamMember.Surname;
            teamMember.About = changedTeamMember.About;
            teamMember.TwitterLink = changedTeamMember.TwitterLink;
            teamMember.InstagramLink = changedTeamMember.InstagramLink;
            teamMember.FacebookLink = changedTeamMember.FacebookLink;
            teamMember.LinkedInLink = changedTeamMember.LinkedInLink;
            teamMember.PositionId = changedTeamMember.PositionId;

            _lumiaDbContext.SaveChanges();

            return RedirectToAction("index", "team");
        }

        public IActionResult Delete(int id)
        {
            TeamMember teamMember = _lumiaDbContext.TeamMembers.FirstOrDefault(x => x.Id == id);

            if (teamMember == null) return View(teamMember);

            string deletedImage = teamMember.ImageName;
            string deletedImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/teammembers", deletedImage);

            if (System.IO.File.Exists(deletedImagePath))
            {
                System.IO.File.Delete(deletedImagePath);
            }

            _lumiaDbContext.TeamMembers.Remove(teamMember);
            _lumiaDbContext.SaveChanges();

            return RedirectToAction("index", "team");
        }
    }
}