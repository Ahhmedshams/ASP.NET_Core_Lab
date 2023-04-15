using Microsoft.EntityFrameworkCore;
using Student_Management_System.Models;
using Student_Management_System.Service.interfaces;

namespace Student_Management_System.Service
{
    public class CoursesRepository : ICourseRepository
    {
        Context context ;

        public CoursesRepository(Context _context)
        {
            context = _context;
        }
        public async Task< List<Course> >GetAll()
        {
            return context.Courses != null ?
                await context.Courses.ToListAsync():new List<Course> { };
        }
        public async Task< Course> GetByID(int id)
        {
            return await context.Courses.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task Create(Course course)
        {
            try
            {
                await context.Courses.AddAsync(course);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g. by logging or returning an error message
            }
        }

        public async Task Delete(int id)
        {
            var crs = await context.Courses.FirstOrDefaultAsync(d => d.Id == id);
            if (crs != null)
            {
                context.Courses.Remove(crs);
                 await context.SaveChangesAsync();
            }
        }

        public async Task Update(Course Course)
        {
            Course old = await context.Courses.FirstOrDefaultAsync(d => d.Id == Course.Id);
            if (old != null)
            {
                old.Name = Course.Name;
                old.Description = Course.Description;
                old.Lect_Hours = Course.Lect_Hours;
                old.Lab_Hours  = Course.Lab_Hours;
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Course>> AllNotMatchWith(IEnumerable<Course> courses)
        {
            var allCourses = await context.Courses.ToListAsync();
            var notMatchingCourses = allCourses.Except(courses, new CourseComparer() );
            return notMatchingCourses.ToList();
        }



       


    }
}
