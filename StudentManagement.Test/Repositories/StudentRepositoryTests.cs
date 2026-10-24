namespace StudentManagement.Repositories
{
    using StudentManagement.Controllers;
    using StudentManagement.Models;

    public class StudentRepositoryTests : IStudentRepositoryTests
    {
        private readonly StudentManagementDbContext _context;

        public StudentRepositoryTests(StudentManagementDbContext context)
        {
            _context = context;
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student? GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
 
        }

        public void Update(Student student)
        {
            Student? existingStudent = _context.Students.Find(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Education = student.Education;
                existingStudent.Semester = student.Semester;
                existingStudent.Email = student.Email;
                _context.SaveChanges();
            }
        }

        public void Delete(Student student)
        {
            Student? existingStudent = _context.Students.Find(student.Id);
            if (existingStudent != null)
            {
                _context.Students.Remove(existingStudent);
                _context.SaveChanges();
            }
        }

       
    }
}
