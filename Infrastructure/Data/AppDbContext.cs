using Domain.Model;
using Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CourseEntityTypeConfiguration().Configure(modelBuilder.Entity<Course>());
            new StudentEntityTypeConfiguration().Configure(modelBuilder.Entity<Student>());
            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new RoleEntityTypeConfiguration().Configure(modelBuilder.Entity<Role>());
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
