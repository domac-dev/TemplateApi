using Domain;
using Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Security
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(ModelConstants.RoleModel.NAME)
                .IsRequired();

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.HasMany(ur => ur.Users)
                .WithMany(ur => ur.Roles)
                .UsingEntity<UserRole>(j => j.ToTable(nameof(UserRole), Schemas.SECURITY));

            builder.Ignore(x => x.Content);
            builder.HasMany(ct => ct.Translations)
                .WithOne()
                .HasForeignKey(ctt => ctt.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(ct => ct.Translations)
             .AutoInclude();

            builder.MapBaseEntity();

            builder.ToTable(nameof(Role), Schemas.SECURITY);
        }
    }
}
