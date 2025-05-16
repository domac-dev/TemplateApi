using Domain;
using Domain.Entities.Core;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Token)
                 .HasMaxLength(ModelConstants.TokenModel.VALUE)
                 .IsRequired();

            builder.Property(x => x.ExpiresAt)
                .IsRequired();

            builder.Property(x => x.RevokedAt);

            builder.Property(x => x.ReplacedByToken)
                .HasMaxLength(ModelConstants.TokenModel.VALUE);

            builder.Property(x => x.CreatedByIp)
                .HasMaxLength(ModelConstants.TokenModel.IP_ADDRESS);

            builder.Property(x => x.RevokedBy)
                .HasMaxLength(ModelConstants.TokenModel.IP_ADDRESS);

            builder.Property(x => x.ReasonRevoked)
                .HasMaxLength(ModelConstants.TokenModel.REVOKE_REASON);

            builder.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.MapBaseEntity();

            builder.ToTable(nameof(RefreshToken));
        }
    }
}
