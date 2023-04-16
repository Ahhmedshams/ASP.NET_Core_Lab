using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Student_Management_System.Models;
using Student_Management_System.Service;
using Student_Management_System.Service.interfaces;

namespace Student_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //built in servers
            //Enable any Class To inject IdentityUser And IdentityRole
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<Context>();


 

            //.GetConnectionString

            //register custom service DI
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IStudentCoursesRepository, StudentCoursesRepository>();
            builder.Services.AddScoped<ICourseRepository, CoursesRepository>();
            builder.Services.AddDbContext<Context>(options =>
            {
                //options.UseSqlServer("Data Source=.;Initial Catalog=ASPLAB;Integrated Security=True;TrustServerCertificate=True;");
               options.UseSqlServer(builder.Configuration.GetConnectionString("Connextion1"));

            });

            



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}