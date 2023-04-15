using Student_Management_System.Models;
using System.Configuration;

namespace Student_Management_System.Service.interfaces
{
    public interface IStudentCoursesRepository:IRepository<StudentCourses>
    {
        Task<StudentCourses> check(int stdID, int crsId);
        Task UpdateDegree(int stdID, int crsId, int Degree);
    }
}
