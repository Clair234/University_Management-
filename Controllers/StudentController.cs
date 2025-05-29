using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Models;
using UniversitySystem.Data;

namespace UniversitySystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Dashboard(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return NotFound();

            ViewBag.UserId = userId; // Pass userId to the view
            return View(user);
        }

        public IActionResult AvailableCourses(int userId)
        {
            // Find courses the student is not yet registered for
            var registeredCourses = _context.StudentCourses
                .Where(sc => sc.StudentId == userId)
                .Select(sc => sc.CourseID)
                .ToList();

            var availableCourses = _context.Courses
                .Where(c => !registeredCourses.Contains(c.CourseID))
                .ToList();

            ViewBag.UserId = userId; // Pass userId to the view
            return View(availableCourses);
        }

        public IActionResult RegisterCourse(int userId, int courseId)
        {
            // Check if this course is already registered for this student
            var exists = _context.StudentCourses
                .Any(sc => sc.StudentId == userId && sc.CourseID == courseId);

            if (!exists)
            {
                // Get the course details
                var course = _context.Courses.Find(courseId);
                if (course == null)
                    return NotFound();

                // Get the user
                var user = _context.Users.Find(userId);
                if (user == null)
                    return NotFound();

                // Create a new student course registration
                _context.StudentCourses.Add(new StudentCourse
                {
                    StudentId = userId,
                    CourseID = courseId,
                    RegistrationDate = DateTime.Now,
                    Student = user
                });

                _context.SaveChanges();
            }

            // Redirect back to the courses view
            return RedirectToAction("Courses", new { userId });
        }

        public IActionResult Courses(int userId)
        {
            // Get all courses registered by this student
            var courses = _context.StudentCourses
                .Include(sc => sc.Course)
                .Where(sc => sc.StudentId == userId)
                .Select(sc => sc.Course)
                .ToList();

            ViewBag.UserId = userId; // Pass userId to the view
            return View(courses);
        }
        
        

        
    }
}

       


        



    
