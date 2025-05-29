using Microsoft.AspNetCore.Mvc;

namespace UniversitySystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Dashboard() => View();
        public IActionResult Calendar() => View();
        public IActionResult Announcements() => View();
    }
}
