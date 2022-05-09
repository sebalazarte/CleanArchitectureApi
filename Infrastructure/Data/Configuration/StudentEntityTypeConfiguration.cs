using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    internal class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(x => x.StudentId).HasName("PK_Student");
            builder.Property(x => x.DocumentNumber).IsRequired();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100).HasColumnType("varchar"); ;
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(200).HasColumnType("varchar"); ;
            builder.Property(x => x.DateOfBirth).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.Address).HasMaxLength(200).HasColumnType("varchar"); ;
            builder.Property(x => x.Phone).HasMaxLength(50).HasColumnType("varchar"); ;
            builder.Property(x => x.Email).HasMaxLength(100).HasColumnType("varchar"); ;
            builder.HasOne(x => x.Course)
                   .WithMany(x => x.Students)
                   .HasForeignKey(x => x.CourseId)
                   .HasConstraintName("FK_Student_Course");
            builder.HasIndex(x => x.DocumentNumber, "UK_Student_DNI").IsUnique();
            builder.HasIndex(x => x.FullName, "UK_Student_FullName");

        }
    }
}
