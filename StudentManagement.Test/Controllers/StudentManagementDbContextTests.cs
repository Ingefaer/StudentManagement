using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentManagementDbContextTests : DbContext
    {
        public StudentManagementDbContextTests(DbContextOptions<StudentManagementDbContextTests> options) : base(options)
        {

        }
        public DbSet<StudentTests> Students { get; set; }
    }
}