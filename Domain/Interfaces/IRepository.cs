namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T item);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, T item);
    }
}
