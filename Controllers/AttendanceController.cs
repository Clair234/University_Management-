using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Models;

namespace UniversitySystem.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            var attendanceList = new List<Attendance>();
            return View(attendanceList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Attendance model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new Attendance();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Attendance model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = new Attendance();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var model = new Attendance();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
