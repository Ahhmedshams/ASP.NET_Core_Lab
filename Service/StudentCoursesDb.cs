using Student_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Student_Management_System.Service
{
    public class StudentCoursesDb
    {
        Context context = new Context();

        public async Task<List<StudentCourses>> GetAll()
        {
            return context.StudentCourses != null ?
                await context.StudentCourses.ToListAsync() : new List<StudentCourses> { };
        }


        public async Task Add(StudentCourses stdCourse)
        {
            try
            {
                await context.StudentCourses.AddAsync(stdCourse);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g. by logging or returning an error message
            }
        }

        //public async Task Delete(int id)
        //{
        //    var crs = await context.Courses.FirstOrDefaultAsync(d => d.Id == id);
        //    if (crs != null)
        //    {
        //        context.Courses.Remove(crs);
        //        await context.SaveChangesAsync();
        //    }
        //}

        //public void Update(Course Course)
        //{
        //    Course old = context.Courses.FirstOrDefault(d => d.Id == Course.Id);
        //    if (old != null)
        //    {
        //        old.Name = Course.Name;
        //        old.Description = Course.Description;
        //        old.Lect_Hours = Course.Lect_Hours;
        //        old.Lab_Hours = Course.Lab_Hours;
        //        context.SaveChanges();

        //    }
        //}

        //public async Task<List<Course>> AllNotMatchWith(IEnumerable<Course> courses)
        //{
        //    var allCourses = await context.Courses.ToListAsync();
        //    var notMatchingCourses = allCourses.Except(courses);
        //    return notMatchingCourses.ToList();
        //}

    }
}
