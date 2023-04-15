using Microsoft.EntityFrameworkCore;
using Student_Management_System.Models;
using Student_Management_System.Service.interfaces;

namespace Student_Management_System.Service
{
    public class DepartmentRepository: IDepartmentRepository
    {
        Context context ;

        public DepartmentRepository(Context _context)
        {
            context = _context;
        }
        public async Task< Department > GetByID(int id)
        {
            return await context.Department.Include(d => d.courses).FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<List<Department> > GetAll()
        {
            return await context.Department.ToListAsync();
        }

        public async Task Create(Department department)
        {
            context.Department.Add(department);
           await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var dept = await context.Department.FirstOrDefaultAsync(d=>d.Id == id);
            if(dept != null)
            {
                context.Department.Remove(dept);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Department department)
        {
            Department old = await context.Department.FirstOrDefaultAsync(d=>d.Id==department.Id);
            if(old != null)
            {
                old.Name = department.Name;
                old.Capacity = department.Capacity;
               await context.SaveChangesAsync();

            }
        }

        public async Task<Department> GetDeptWithCourses(int id)
        {
            return await context.Department.Include(d => d.courses).FirstOrDefaultAsync(d => d.Id == id);
        }


        public async Task SaveChanges()
        {
            
            context.SaveChanges();

        }

        public async Task<Department> GetDeptWithStudent(int id)
        {
            return await context.Department.Include(d => d.Students).FirstOrDefaultAsync(d => d.Id == id);
        }



    }
}
