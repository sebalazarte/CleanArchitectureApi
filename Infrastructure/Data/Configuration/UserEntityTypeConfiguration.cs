using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.UserId).HasName("PK_User");
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100).HasColumnType("varchar");
            builder.Property(x => x.RoleId).IsRequired();
            builder.HasOne(x => x.Role)
                   .WithMany(i => i.Users)
                   .HasForeignKey(x => x.RoleId)
                   .HasConstraintName("FK_User_Role");
            builder.HasIndex(x => x.UserName, "UK_User_UserName").IsUnique();
        }
    }
}
