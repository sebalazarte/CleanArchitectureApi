using Domain.Interfaces;
using Domain.Model;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Course> CreateAsync(Course item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _context.Courses.SingleAsync(i => i.CourseId == id);
            _context.Remove(record);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync(int year)
        {
            return await _context.Courses.Where(i => i.Year == year).ToListAsync();
        }

        public async Task<Course> GetAsync(int id)
        {
            return await _context.Courses.SingleAsync(i => i.CourseId == id);
        }

        public async Task UpdateAsync(int id, Course item)
        {
            var record = await _context.Courses.SingleAsync(i => i.CourseId == id);
            record.Level = item.Level;
            record.Shift = item.Shift;
            record.Year = item.Year;
            record.Number = item.Number;
            record.Division = item.Division;
            await _context.SaveChangesAsync();
        }
    }
}
