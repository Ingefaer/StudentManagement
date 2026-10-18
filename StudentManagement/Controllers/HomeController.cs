using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models; 


namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentManagementDbContext _context;

        public HomeController(StudentManagementDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        public IActionResult CreateStudent()
        {
            return View(new Student());
        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
             if (!ModelState.IsValid)
            {
                return View(student);
            }

            _context.Students.Add(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EditStudent(int id)
        {
           Student? student = _context.Students.Find(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            Student? existingStudent = _context.Students.Find(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Education = student.Education;
                existingStudent.Semester = student.Semester;
                existingStudent.Email = student.Email;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteStudent(int id)
        {
            Student? student = _context.Students.Find(id);

            return View(student);
        }

        [HttpPost]
        public IActionResult DeleteStudent(Student student)
        {
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
}
}
