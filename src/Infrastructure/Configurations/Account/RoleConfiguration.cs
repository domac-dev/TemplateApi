using Domain;
using Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Account
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(ModelConstants.RoleModel.NAME)
                .IsRequired();

            builder.HasMany(ur => ur.Users)
                .WithMany(ur => ur.Roles)
                .UsingEntity<UserRole>(j => j.ToTable(nameof(UserRole)));

            builder.MapBaseEntity();

            builder.ToTable(nameof(Role));
        }
    }
}
