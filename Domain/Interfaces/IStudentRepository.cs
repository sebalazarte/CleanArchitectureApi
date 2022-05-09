using Domain.Model;

namespace Domain.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllByCourseAsync(int courseId);
        Task<Student> GetByDocumentNumberAsync(int documentNumber);
    }
}
