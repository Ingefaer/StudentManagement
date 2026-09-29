using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public string Email { get; set; }


    }
}
