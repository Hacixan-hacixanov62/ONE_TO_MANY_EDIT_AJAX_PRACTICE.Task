using Microsoft.EntityFrameworkCore;
using ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Models;

namespace ONE_TO_MANY_EDIT_AJAX_PRACTICE_Task.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }



    }
}
