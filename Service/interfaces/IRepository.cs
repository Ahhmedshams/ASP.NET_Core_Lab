using Student_Management_System.Models;

namespace Student_Management_System.Service
{
    public interface IRepository<T>
    {
        Task<T> GetByID(int id);
        Task<List<T>> GetAll();
        Task Create(T obj);
        Task Delete(int id);
        Task Update(T obj);





    }
}
