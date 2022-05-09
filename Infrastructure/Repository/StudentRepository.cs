using Domain.Interfaces;
using Domain.Model;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateAsync(Student item)
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

        public async Task<IEnumerable<Student>> GetAllByCourseAsync(int courseId)
        {
            return await _context.Students
                .Where(i => i.CourseId == courseId)
                .OrderBy(i => i.FullName)
                .ToListAsync();
        }

        public async Task<Student> GetAsync(int id)
        {
            return await _context.Students.SingleAsync();
        }

        public async Task<Student> GetByDocumentNumberAsync(int documentNumber)
        {
            return await _context.Students.SingleOrDefaultAsync(i => i.DocumentNumber == documentNumber);
        }

        public async Task UpdateAsync(int id, Student item)
        {
            var record = await _context.Students.SingleAsync(_context => _context.StudentId == id);
            record.DocumentNumber = item.DocumentNumber;
            record.CourseId = item.CourseId;
            record.FirstName = item.FirstName.TrimEnd();
            record.LastName = item.LastName.TrimEnd();
            record.Email = item.Email;
            record.Phone = item.Phone;
            record.DateOfBirth = item.DateOfBirth;
            record.Address = item.Address;
            record.FullName = $"{record.LastName} {record.FirstName}";
            await _context.SaveChangesAsync();
        }
    }
}
