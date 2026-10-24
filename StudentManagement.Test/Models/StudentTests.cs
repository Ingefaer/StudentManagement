using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class StudentTests
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Udfyld venligst navn.")]
        public string Name { get; set; }
        [Required (ErrorMessage = "Udfyld venligst uddannelse.")]
        public string Education { get; set; }
        [Required (ErrorMessage = "Udfyld venligst semester.")]
        public int? Semester { get; set; }
        [Required (ErrorMessage = "Udfyld venligst email.")]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> semesters()
        {
         return new List<SelectListItem>() { 
        new SelectListItem() { Text = "1", Value = "1" },
        new SelectListItem() { Text = "2", Value = "2" },
        new SelectListItem() { Text = "3", Value = "3" },
        new SelectListItem() { Text = "4", Value = "4" },
        new SelectListItem() { Text = "5", Value = "5" }
         };

        }
    }
}
