using Domain.Interfaces;
using Domain.Model;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var record = await GetAsync(id);
            _context.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users
                .Include(i => i.Role)
                .SingleAsync(i => i.UserId == id);
        }

        public async Task UpdateAsync(int id, User item)
        {
            var record = await GetAsync(id);
            record.FullName = item.FullName;
            record.Email = item.Email;
            record.RoleId = item.RoleId;
            await _context.SaveChangesAsync();
        }

        public async Task<User> ValidateAsync(string userName, string password)
        {
            return await _context.Users
                .Include(i => i.Role)
                .FirstOrDefaultAsync(i => i.UserName.ToLower() == userName.ToLower() && i.Password == password);
        }
    }
}