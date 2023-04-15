using Microsoft.EntityFrameworkCore;
using Student_Management_System.Models;

namespace Student_Management_System.Service
{
    public class StudentRepository:IStudentRepository
    {
        Context context;

        public StudentRepository(Context _context)
        {
            context = _context;
        }
        public async Task< List<Student> > GetAll()
        {
            return await context.Students.Include(d=>d.Department).ToListAsync();
        }
        public async Task< Student> GetByID(int id)
        {
            return await context.Students.Include(d=>d.Department).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task Create(Student std)
        {
            context.Students.Add(std);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var std = await context.Students.FirstOrDefaultAsync(d => d.Id == id);
            if (std != null)
            {
                context.Students.Remove(std);
               await context.SaveChangesAsync();
            }
        }

        public async Task Update(Student std)
        {
            Student old = await context.Students.FirstOrDefaultAsync(d => d.Id == std.Id);
            if (old != null)
            {
                old.Name = std.Name;
                old.Email = std.Email;
                old.Age= std.Age;
                old.Password =std.Password;
                old.DepartmentId = std.DepartmentId;
                context.SaveChangesAsync();
            }
        }
    }
}
