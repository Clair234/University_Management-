using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Models;

namespace UniversitySystem.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var errorModel = new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier
            };

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the page you requested could not be found.";
                    break;
                default:
                    ViewBag.ErrorMessage = "An unexpected error occurred.";
                    break;
            }

            return View("Error", errorModel);
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }
}
