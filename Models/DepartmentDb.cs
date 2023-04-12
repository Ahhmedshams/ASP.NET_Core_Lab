using Microsoft.EntityFrameworkCore;

namespace Student_Management_System.Models
{
    public class DepartmentDb
    {
        Context context = new Context();

        public List<Department> GetAll()
        {
            return context.Department.ToList();
        }
        public Department GetByID(int id)
        {
            return context.Department.FirstOrDefault(d => d.Id == id);
        }

        public void Add(Department department)
        {
            context.Department.Add(department);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dept = context.Department.FirstOrDefault(d=>d.Id == id);
            if(dept != null)
            {
                context.Department.Remove(dept);
                context.SaveChanges();
            }
        }

        public void Update(Department department)
        {
            Department old = context.Department.FirstOrDefault(d=>d.Id==department.Id);
            if(old != null)
            {
                old.Name = department.Name;
                old.Capacity = department.Capacity;
                context.SaveChanges();

            }
        }

        public void AddCourse(int DeptId, Course course)
        {
           var dept= context.Department.FirstOrDefault(d => d.Id == DeptId); 
            if(dept != null)
            {
                dept.courses.Add(course);
            }
        }
    }
}
