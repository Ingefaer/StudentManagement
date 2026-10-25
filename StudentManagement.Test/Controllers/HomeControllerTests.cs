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

        [Fact]
        public void CreateStudent_ReturnsViewResult_WithEmptyStudent()
        {
            //arrange - sæt en ny liste med studerende
            Mock<IStudentRepository> mock = new();
  

            IStudentRepository repository = mock.Object;
            HomeController controller = new HomeController(repository);

            //Act - henter indexfunktionen fra HomeController
            IActionResult result = controller.CreateStudent();

            // Assert - tjekker om resultatet er en ViewResult og om modellen indeholder den forventede liste af studerende
            ViewResult view = Assert.IsType<ViewResult>(result);

            Student student =
                Assert.IsType<Student>(view.Model);

            //tjekker at studerende er tom
            Assert.Equal(0, student.Id);
            Assert.Null(student.Name);
            Assert.Null(student.Education);
            Assert.Null(student.Semester);
            Assert.Null(student.Email);


        }
        [Fact]
        public void CreateStudent_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            Mock<IStudentRepository> mock = new();
            HomeController controller = new HomeController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            Student student = new Student { Id = 1};

            // Act
            IActionResult result = controller.CreateStudent(student);

            // Assert
            ViewResult view = Assert.IsType<ViewResult>(result); //returnes et viewresult
            Student model = Assert.IsType<Student>(view.Model); //tjekker at modellen på view result er en student
            Assert.Equal(student.Id, model.Id); //tjekker at id er det samme som den studerende der blev sendt ind
            Assert.Null(model.Name); //tjekker at name er null, da der er en fejl i modelstate
            mock.Verify(r => r.Update(It.IsAny<Student>()), Times.Never); //Tester at update ikke køres når der er fejl

        }
        [Fact]
        public void CreateStudent_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            Mock<IStudentRepository> mock = new();
            HomeController controller = new HomeController(mock.Object);
            Student student = new Student
            {
                Id = 1,
                Name = "Lisa",
                Education = "Datamatiker",
                Semester = 3,
                Email = "Lisa@example.com"
            };

            //Act
            IActionResult result = controller.CreateStudent(student);

            //Assert
            mock.Verify(r => r.Update(student), Times.Once); //tjekker at Add bliver kaldt en gang
            RedirectToActionResult redirect = Assert.IsType<RedirectToActionResult>(result); //Tjekker at der redirectes
            Assert.Equal("Index", redirect.ActionName); //tjekker den redirecter til index
        }

        [Fact]
        public void EditStudent_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            Mock<IStudentRepository> mock = new();

            HomeController controller = new HomeController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            Student student = new Student { Id = 1 };

            // Act
            IActionResult result = controller.EditStudent(student);

            // Assert
            ViewResult view = Assert.IsType<ViewResult>(result); //tjekker at resultatet er et view
            Student model = Assert.IsType<Student>(view.Model); //tjekker at modellen på view er en student
            Assert.Equal(student.Id, model.Id); //tjekker at id er det samme som den studerende der blev sendt ind
            Assert.Null(model.Name); //tjekker at name er null, da der er en fejl i modelstate
            mock.Verify(r => r.Update(It.IsAny<Student>()), Times.Never); //tjekker at update ikke køres når der er en fejl
        }
    }
}
