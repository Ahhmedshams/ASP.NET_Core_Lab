using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Student_Management_System.Models
{
    public class Context: DbContext
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ASPLAB;Integrated Security=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourses>().HasKey(a=>new {a.StudentId, a.CourseId});

            modelBuilder.Entity<Course>().HasKey(a => a.Id);
            modelBuilder.Entity<Course>().Property(a => a.Name).IsRequired().HasMaxLength(10);
            base.OnModelCreating(modelBuilder);
        }
    }
}
