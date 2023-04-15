using Student_Management_System.Models;

namespace Student_Management_System.Service
{
    public interface ICourseRepository:IRepository<Course>
    {
        Task<List<Course>> AllNotMatchWith(IEnumerable<Course> courses);
    }
}
