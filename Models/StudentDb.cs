using Microsoft.EntityFrameworkCore;

namespace Student_Management_System.Models
{
    public class StudentDb
    {
        Context context = new Context();

        public List<Student> GetAll()
        {
            return context.Students.Include(d=>d.Department).ToList();
        }
        public Student GetByID(int id)
        {
            return context.Students.Include(d=>d.Department).FirstOrDefault(d => d.Id == id);
        }

        public void Add(Student std)
        {
            context.Students.Add(std);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var std = context.Students.FirstOrDefault(d => d.Id == id);
            if (std != null)
            {
                context.Students.Remove(std);
                context.SaveChanges();
            }
        }

        public void Update(Student std)
        {
            Student old = context.Students.FirstOrDefault(d => d.Id == std.Id);
            if (old != null)
            {
                old.Name = std.Name;
                old.Email = std.Email;
                old.Age= std.Age;
                old.Password =std.Password;
                old.DepartmentId = std.DepartmentId;
                context.SaveChanges();
            }
        }
    }
}
