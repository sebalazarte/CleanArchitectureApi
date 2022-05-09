using Domain.Model;

namespace Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<IEnumerable<Role>> GetAllAsync();
    }
}
