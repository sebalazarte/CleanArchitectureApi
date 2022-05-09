using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    internal class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");
            builder.HasKey(x => x.CourseId).HasName("PK_Course");
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Division).IsRequired();
            builder.Property(x => x.Level).IsRequired().HasMaxLength(1).HasColumnType("varchar");
            builder.Property(x => x.Shift).IsRequired().HasMaxLength(1).HasColumnType("varchar");
            builder.HasMany(x => x.Students);
        }
    }
}
