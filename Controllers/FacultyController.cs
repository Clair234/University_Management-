using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Models;
using UniversitySystem.Data;


namespace UniversitySystem.Controllers
{
    public class FacultyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Faculties.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null) return NotFound();
            return View(faculty);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Faculties.Add(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null) return NotFound();
            return View(faculty);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Faculties.Update(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null) return NotFound();
            return View(faculty);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            _context.Faculties.Remove(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Dashboard(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return NotFound();

            return View(user);
        }



        public IActionResult Courses(int userId)
        {
            Console.WriteLine("UserID: " + userId); // أو Debug.WriteLine

            var courses = _context.Courses
                .Where(c => c.InstructorID == userId)
                .ToList();

            return View(courses);
        }

        public IActionResult ViewCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
                return NotFound();

            return View(course);
        }
        public IActionResult EditCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Courses", new { userId = course.InstructorID });
            }

            return View(course);
        }
        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("DeleteCourse")]
        public async Task<IActionResult> DeleteCourseConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Courses", new { userId = course.InstructorID });
        }

        public IActionResult MyCourses(int facultyId)
        {
            var courses = _context.Courses
                .Where(c => c.InstructorID == facultyId)
                .ToList();

            return View(courses);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login( string Password)
        {
            var faculty = _context.Admins.FirstOrDefault(a => a.Password == Password);

            if (faculty != null)
            {
                // تسجيل الدخول ناجح - حفظ الإيميل في Session
                HttpContext.Session.SetString("AdminPassword", faculty.Password);

                // التوجيه إلى Index
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Invalid Password";
            return View();
        }



    }
}
