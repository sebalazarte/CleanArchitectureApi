using Domain.Interfaces;
using Domain.Model;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Role> CreateAsync(Role item)
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

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetAsync(int id)
        {
            return await _context.Roles.SingleAsync(i => i.RoleId == id);
        }

        public async Task UpdateAsync(int id, Role item)
        {
            var record = await GetAsync(id);
            record.RoleName = item.RoleName;
            await _context.SaveChangesAsync();
        }
    }
}
