using Student_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Student_Management_System.Service.interfaces;

namespace Student_Management_System.Service
{
    public class StudentCoursesRepository : IStudentCoursesRepository
    {
        Context context;

        public StudentCoursesRepository(Context _context)
        {
            context = _context;
        }

        public async Task<StudentCourses> GetByID(int id )
        {
            throw new NotImplementedException();

        }
        public async Task<List<StudentCourses>> GetAll()
        {
            return context.StudentCourses != null ?
                await context.StudentCourses.ToListAsync() : new List<StudentCourses> { };
        }


        public async Task Create(StudentCourses stdCourse)
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

        public async Task UpdateDegree (int stdID, int crsId, int Degree)
        {
            StudentCourses old = await context.StudentCourses.FirstOrDefaultAsync(s => s.StudentId == stdID && s.CourseId == crsId);
            if (old != null)
            {
                old.Degree= Degree;
                await context.SaveChangesAsync();

            }
        }



        public async Task<StudentCourses> check( int stdID , int crsId)
        {
            return await context.StudentCourses.FirstOrDefaultAsync(s => s.StudentId == stdID && s.CourseId == crsId);
        }
        public async Task Delete(int id)
        {
           throw new NotImplementedException();
        }

      

     

        public Task Update(StudentCourses obj)
        {
            throw new NotImplementedException();
        }

       

    }
}
