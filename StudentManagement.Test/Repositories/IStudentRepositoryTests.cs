namespace StudentManagement.Repositories
{
    using StudentManagement.Models;

    public interface IStudentRepositoryTests
    {
        List<Student> GetAll();
        Student? GetById(int id);
        void Add(Student student);
        void Update(Student student);
        void Delete(Student student);
    }
}
