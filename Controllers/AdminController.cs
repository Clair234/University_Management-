using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Data;
using UniversitySystem.Models;

namespace UniversitySystem.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminEmail")))
            {
                return RedirectToAction("Login");
            }

            var admins = _context.Admins.ToList();
            return View(admins);
        }


        // CreateCourse
        public IActionResult CreateCourse()
        {
            var instructors = _context.Faculties.ToList();

            ViewBag.Instructors = instructors.Select(i => new SelectListItem
            {
                Text = $"{i.Name} - {i.UserID}",
                Value = $"{i.UserID}|{i.Name}"
            }).ToList();

            return View();
        }

        // POST: CreateCourse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return RedirectToAction("ManageCourses"); // أو أي صفحة رجوع بعد الحفظ
            }

            // لو في خطأ في الفورم، نرجع نجهز المدرسين تاني علشان تظهر في الصفحة
            var instructors = _context.Faculties.ToList();
            ViewBag.Instructors = instructors.Select(i => new SelectListItem
            {
                Text = $"{i.Name} - {i.UserID}",
                Value = $"{i.UserID}|{i.Name}"
            }).ToList();

            return View(course);
        }



        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
                return NotFound();

            // جلب المدرسين للقائمة
            var instructors = _context.Faculties.ToList();
            ViewBag.Instructors = instructors.Select(i => new SelectListItem
            {
                Text = $"{i.Name} - {i.FacultyID}",
                Value = i.FacultyID.ToString()
            }).ToList();

            return View(course);
        }



        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCourse(Course course)
        {
            var existingCourse = _context.Courses.FirstOrDefault(c => c.CourseID == course.CourseID);
            if (existingCourse == null)
                return NotFound();

            // تحديث البيانات
            existingCourse.CourseName = course.CourseName;
            existingCourse.CourseDescription = course.CourseDescription;
            existingCourse.CreditHours = course.CreditHours;
            existingCourse.Department = course.Department;
            existingCourse.Semaster = course.Semaster;
            existingCourse.MaxCapcity = course.MaxCapcity;
            existingCourse.InstructorID = course.InstructorID;

            // تحديث اسم المدرس بناءً على الـ ID
            var instructor = _context.Faculties.FirstOrDefault(i => i.FacultyID == course.InstructorID);
            if (instructor != null)
            {
                existingCourse.InstructorName = instructor.Name;
            }

            _context.SaveChanges();
            return RedirectToAction("ManageCourses");
        }


        /// <summary>
        /// ///////////////////////////
        /// </summary>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var model = new Admin();
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = new Admin();
            return View(model);
        }

        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return RedirectToAction("ManageCourses");
        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("ConfirmDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("ManageCourses");
        }



        [HttpGet]
        public IActionResult ConfirmDeleteS(int userid)
        {
            var student = _context.Students.FirstOrDefault(s => s.UserID == userid);
            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost, ActionName("ConfirmDeleteS")]
        public IActionResult DeleteConfirm(int userid)
        {
            var student = _context.Students.FirstOrDefault(s => s.UserID == userid);
            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("ManageStudents"); // Make sure this is "ManageStudents" not "ManageStdents"
        }


        public IActionResult ManageCourses()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }



        public IActionResult ManageStudents()
        {
            var students = _context.Students.ToList();
            return View(students);
        }


        public IActionResult StudentDetails(int userid)
        {
            var student = _context.Students .FirstOrDefault(s => s.UserID == userid);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


        public IActionResult FacultyDetails(int id)
        {
            var faculty = _context.Faculties.FirstOrDefault(f => f.UserID == id);

            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }



        // New action method to handle ManageFaculties route
        public IActionResult ManageFaculties()
        {
            var faculties = _context.Faculties.ToList();

            return View(faculties);
        }

        /// //////CreateFaculty
        [HttpGet]
        public IActionResult CreateFaculty()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFaculty(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Faculties.Add(faculty);
                _context.SaveChanges();
                return RedirectToAction("ManageFaculties");
            }
            return View(faculty);
        }


        /// //////CreateFaculty
        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("ManageStudents");
            }
            return View(student);
        }




        [HttpGet]
        public IActionResult ConfirmDeleteF(int id)
        {
            var faculty = _context.Faculties.FirstOrDefault(f => f.UserID == id);
            if (faculty == null)
                return NotFound();

            return View(faculty);
        }

        [HttpPost, ActionName("ConfirmDeleteF")]
        public IActionResult DeleteConfirmf(int id)
        {
            var faculty = _context.Faculties.FirstOrDefault(f => f.UserID == id);
            if (faculty == null)
                return NotFound();

            _context.Faculties.Remove(faculty);
            _context.SaveChanges();
            return RedirectToAction("ManageFaculties"); 
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email,string Password, string access)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Email == email && a.Password== Password);

            if (admin != null)
            {
                // تسجيل الدخول ناجح - حفظ الإيميل في Session
                HttpContext.Session.SetString("AdminEmail", admin.Email);
                HttpContext.Session.SetString("AdminPassword", admin.Password);

                // التوجيه إلى Index
                return RedirectToAction("Index");
            }

            if (access != "secure111")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Email";
            return View();
        }


        [HttpGet]
        public IActionResult EditStudent(int userid)
        {
            var student = _context.Students.FirstOrDefault(s => s.UserID == userid);
            if (student == null)
                return NotFound();


            return View(student);
        }

        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudent(Student student)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.UserID == student.UserID);
            if (existingStudent == null)
                return NotFound();

            // تحديث البيانات
            existingStudent.FullName = student.FullName;
            existingStudent.GPA = student.GPA;
            existingStudent.Address = student.Address;
            existingStudent.City = student.City;
            existingStudent.DateOfBirth = student.DateOfBirth;
            existingStudent.AdvisorID = student.AdvisorID;
            existingStudent.NationalID = student.NationalID;

            existingStudent.Email = student.Email;
            existingStudent.Phone = student.Phone;
            existingStudent.Gender = student.Gender;



            _context.SaveChanges();
            return RedirectToAction("ManageStudents");
        }

        public IActionResult EditFaculty(int id)
        {
            var faculty = _context.Faculties.FirstOrDefault(f => f.UserID == id);
            if (faculty == null)
                return NotFound();

           
            return View(faculty);
        }



        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFaculty(Faculty faculty)
        {
            var existingFaculty = _context.Faculties.FirstOrDefault(f => f.UserID == faculty.UserID);
            if (existingFaculty == null)
                return NotFound();

            // تحديث البيانات
            existingFaculty.Name = faculty.Name;
            existingFaculty.Title = faculty.Title;
            existingFaculty.Department = faculty.Department;
            existingFaculty.Email = faculty.Email;
            existingFaculty.phone = faculty.phone;
            existingFaculty.Gender = faculty.Gender;


            // تحديث اسم المدرس بناءً على الـ ID
            var instructor = _context.Faculties.FirstOrDefault(i => i.UserID == faculty.UserID);
            if (instructor != null)
            {
                existingFaculty.Name = instructor.Name;
            }

            _context.SaveChanges();
            return RedirectToAction("ManageFaculties");
        }




    }
}
