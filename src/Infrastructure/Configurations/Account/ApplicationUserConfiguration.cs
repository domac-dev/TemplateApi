using Domain;
using Domain.Entities.Core;
using Infrastructure;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id);
            builder.HasIndex(u => u.Id);

            builder.Property(u => u.Culture)
                .IsRequired();

            builder.Property(u => u.EmailConfirmed)
                .HasDefaultValue(false);

            builder.Property(u => u.FullName)
                .HasMaxLength(ModelConstants.GeneralModel.MAX)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(ModelConstants.GeneralModel.MAX);

            builder.Property(u => u.PasswordHash);

            builder.HasMany(ur => ur.Roles)
                .WithMany(ur => ur.Users)
                .UsingEntity<UserRole>(j => j.ToTable(nameof(UserRole)));

            builder.HasMany(ur => ur.Claims)
                .WithMany(ur => ur.Users)
                .UsingEntity<UserClaim>(j => j.ToTable(nameof(UserClaim)));

            builder.Property(u => u.EmailConfirmationToken)
                .HasDefaultValueSql(SQLConstants.NEWID);

            builder.HasIndex(c => c.Id);

            builder.MapBaseEntity();

            builder.ToTable(nameof(ApplicationUser));
        }
    }
}
