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
            return View();
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

            return RedirectToAction("ListStudents");
        }

        public IActionResult ListStudents()
        {
            var students = _context.Students.ToList();
            return View(students);
        }
    }
}
