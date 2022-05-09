using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    internal class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(x => x.RoleId).HasName("PK_Role");
            builder.Property(x => x.RoleName).IsRequired().HasMaxLength(50).HasColumnType("varchar");
            builder.HasMany(x => x.Users);
            builder.HasIndex(x => x.RoleName).IsUnique();
        }
    }
}
