using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Models;

namespace UniversitySystem.Controllers
{
    public class EnrollmentController : Controller
    {
        public IActionResult Index()
        {
            var enrollments = new List<Enrollment>();
            return View(enrollments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Enrollment model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new Enrollment();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Enrollment model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = new Enrollment();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var model = new Enrollment();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
