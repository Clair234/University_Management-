using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;
using UniversitySystem.ViewModels;

namespace UniversitySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                switch (user.Role)
                {
                    case "Student":
                        return RedirectToAction("Dashboard", "Student", new { userId = user.Id });
                    case "Teacher":
                        return RedirectToAction("Dashboard", "Faculty", new { userId = user.Id });
                    case "Admin":
                        return RedirectToAction("Index", "Admin", new { userId = user.Id });
                    default:
                        return RedirectToAction("Login");
                }
            }
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                // If the user is registering as a student, create a student record
                if (model.Role == "Student")
                {
                    var student = new Student
                    {
                        FullName = model.Username, // You might want to add FirstName/LastName to your RegisterViewModel
                        Email = model.Email,
                        UserID = user.Id,
                        // Set default values for other required fields
                        Phone = "",
                        NationalID = "", // You might want to add this to your registration form
                        DateOfBirth = DateTime.Now, // Default value, should be collected during registration
                        Gender = "", // Should be collected during registration
                        Address = "",
                        City = "",
                        GPA = 0.0,
                        TotalCreditHours = 0,
                        EnrollmentStatus = "Active",
                        AdvisorID = 0 // Set a default advisor or make it nullable
                    };

                    _context.Students.Add(student);
                    _context.SaveChanges();
                }

                if (model.Role == "Teacher")
                {
                    var faculty = new Faculty
                    {
                        Name = model.Username,
                        Email = model.Email,
                        phone = "", 
                        Department = "",
                        UserID = user.Id,
                        Gender = "",
                        Title=""
                    };

                    _context.Faculties.Add(faculty);
                    _context.SaveChanges();
                }



                switch (model.Role)
                {
                    case "Student":
                        return RedirectToAction("Dashboard", "Student", new { userId = user.Id });
                    case "Teacher":
                        return RedirectToAction("Dashboard", "Faculty", new { userId = user.Id });
                    case "Admin":
                        return RedirectToAction("Index", "Admin", new { userId = user.Id });
                    default:
                        return RedirectToAction("Login");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            // Placeholder for logout logic
            return RedirectToAction("Login");
        }
    }
}