using Domain;
using Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Security
{
    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(ModelConstants.ClaimModel.NAME)
                .IsRequired();

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.HasMany(ur => ur.Users)
                .WithMany(ur => ur.Claims)
                .UsingEntity<UserClaim>(j => j.ToTable(nameof(UserClaim), Schemas.SECURITY));

            builder.Ignore(x => x.Content);
            builder.HasMany(ct => ct.Translations)
                .WithOne()
                .HasForeignKey(ctt => ctt.ClaimId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(ct => ct.Translations)
             .AutoInclude();

            builder.MapBaseEntity();

            builder.ToTable(nameof(Claim), Schemas.SECURITY);
        }
    }
}
