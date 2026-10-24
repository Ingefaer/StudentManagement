using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentManagement.Models;
using StudentManagement.Repositories;


namespace StudentManagement.Controllers
{
    public class HomeControllerTests : Controller
    {

        [Fact]
        public void Index_ReturnsViewResult_WithListOfStudents()
        {
            //arrange - sæt en ny liste med studerende
            Mock<IStudentRepository> mock = new();
            mock.Setup(r => r.GetAll()).Returns(new List<Student> {
                new() { Id = 0, Name ="Lisa" },
                new() { Id = 1, Name = "Mette" },
                new() { Id = 2, Name = "Jane" },
             });

            IStudentRepository repository = mock.Object;
            HomeController controller = new HomeController(repository);

            //Act - henter indexfunktionen fra HomeController
            IActionResult result = controller.Index();

            // Assert - tjekker om resultatet er en ViewResult og om modellen indeholder den forventede liste af studerende
            ViewResult view = Assert.IsType<ViewResult>(result);

            List<Student> students =
                Assert.IsType<List<Student>>(view.Model);

            Assert.Equal(3, students.Count);
            Assert.Equal("Lisa", students[0].Name);
            Assert.Equal("Mette", students[1].Name);
            Assert.Equal("Jane", students[2].Name);

        }

        [Theory] // henter detaljer om studerende
        [InlineData(0, "Lisa")]
        [InlineData(1, "Mette")]
        [InlineData(2, "Jane")]
        public void Details_returns_a_view_with_the_student_that_has_the_id(int id, string expectedName)
        {
            // Arrange
            Mock<IStudentRepository> mock = new();

            mock.Setup(r => r.GetById(0))
                .Returns(new Student { Id = 0, Name = "Lisa" });

            mock.Setup(r => r.GetById(1))
                .Returns(new Student { Id = 1, Name = "Mette" });

            mock.Setup(r => r.GetById(2))
                .Returns(new Student { Id = 2, Name = "Jane" });
            HomeController controller = new HomeController(mock.Object);

            // Act
            IActionResult result = controller.EditStudent(id);

            // Assert
            ViewResult view = Assert.IsType<ViewResult>(result);
            Student student = Assert.IsType<Student>(view.Model);

            Assert.Equal(id, student.Id);
            Assert.Equal(expectedName, student.Name);
        }
    }
}
