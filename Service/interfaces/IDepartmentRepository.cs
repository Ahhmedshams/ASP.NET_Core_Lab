using Student_Management_System.Models;

namespace Student_Management_System.Service
{
    public interface IDepartmentRepository:IRepository<Department>
    {
        Task<Department> GetDeptWithCourses(int id);
        Task SaveChanges();
        Task<Department> GetDeptWithStudent(int id);

    }
}
