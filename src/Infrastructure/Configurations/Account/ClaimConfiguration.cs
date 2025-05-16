using Domain;
using Domain.Entities.Core;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(ModelConstants.ClaimModel.NAME)
                .IsRequired();

            builder.HasMany(ur => ur.Users)
                .WithMany(ur => ur.Claims)
                .UsingEntity<UserClaim>(j => j.ToTable(nameof(UserClaim)));

            builder.MapBaseEntity();

            builder.ToTable(nameof(Claim));
        }
    }
}
