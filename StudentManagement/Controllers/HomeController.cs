using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Repositories;


namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _repository;

        public HomeController(IStudentRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var students = _repository.GetAll();
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

            _repository.Add(student);

            return RedirectToAction("Index");
        }

        public IActionResult EditStudent(int id)
        {
           Student? student = _repository.GetById(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

          
                _repository.Update(student);
           
            return RedirectToAction("Index");
        }

        public IActionResult DeleteStudent(int id)
        {
            Student? student = _repository.GetById(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult DeleteStudent(Student student)
        {
            if (student != null)
            {
                _repository.Delete(student);
            }
            return RedirectToAction("Index");
        }
}
}
