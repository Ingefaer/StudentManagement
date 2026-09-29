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
            return View();
        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
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
