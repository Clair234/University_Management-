using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Models;

namespace UniversitySystem.Controllers
{
    public class AnnouncementController : Controller
    {
        public IActionResult Index()
        {
            var announcements = new List<Announcement>();
            return View(announcements);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Announcement model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new Announcement();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Announcement model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = new Announcement();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var model = new Announcement();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
