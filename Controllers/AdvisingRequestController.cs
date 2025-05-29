using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Models;

namespace UniversitySystem.Controllers
{
    public class AdvisingRequestController : Controller
    {
        public IActionResult Index()
        {
            var requests = new List<AdvisingRequest>();
            return View(requests);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AdvisingRequest model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new AdvisingRequest();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AdvisingRequest model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = new AdvisingRequest();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var model = new AdvisingRequest();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
