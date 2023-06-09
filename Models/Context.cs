﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using Student_Management_System.Models.ViewModel;

namespace Student_Management_System.Models
{
    public class Context:IdentityDbContext
    {
        public Context(DbContextOptions options) : base(options)
        { }
        public DbSet<Department> Department { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourses> StudentCourses { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ASPLAB;Integrated Security=True;TrustServerCertificate=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourses>().HasKey(a=>new {a.StudentId, a.CourseId});
            modelBuilder.Entity<Course>().HasKey(a => a.Id);
            modelBuilder.Entity<Course>().Property(a => a.Name).IsRequired().HasMaxLength(10);
            base.OnModelCreating(modelBuilder);
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ASPLAB;Integrated Security=True;TrustServerCertificate=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}


        public DbSet<Student_Management_System.Models.ViewModel.RegistrationViewModel>? RegistrationViewModel { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ASPLAB;Integrated Security=True;TrustServerCertificate=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}


        public DbSet<Student_Management_System.Models.ViewModel.LoginViewModel>? LoginViewModel { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ASPLAB;Integrated Security=True;TrustServerCertificate=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}


        public DbSet<Student_Management_System.Models.ViewModel.RoleViewModel>? RoleViewModel { get; set; }
    }
}
