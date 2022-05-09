using Domain.Model;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> ValidateAsync(string userName, string password);
    }
}
