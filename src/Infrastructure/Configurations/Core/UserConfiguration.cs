using Domain;
using Domain.Entities.Core;
using Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Core
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Culture)
                .IsRequired();

            builder.Property(u => u.Telephone)
               .HasMaxLength(ModelConstants.GeneralModel.TELEPHONE)
               .IsRequired();

            builder.Property(u => u.EmailConfirmed)
                .HasDefaultValue(false);

            builder.Property(u => u.FullName)
                .HasMaxLength(ModelConstants.GeneralModel.MAX)
                .IsRequired();

            builder.Ignore(ct => ct.Culture);


            builder.Property(u => u.Email)
                .HasMaxLength(ModelConstants.GeneralModel.MAX);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(ModelConstants.GeneralModel.MAX);

            builder.HasMany(ur => ur.Roles)
                .WithMany(ur => ur.Users)
                .UsingEntity<UserRole>(j => j.ToTable(nameof(UserRole), Schemas.SECURITY));

            builder.HasMany(ur => ur.Claims)
                .WithMany(ur => ur.Users)
                .UsingEntity<UserClaim>(j => j.ToTable(nameof(UserClaim), Schemas.SECURITY));

            builder.Property(u => u.EmailConfirmationToken)
                .HasDefaultValueSql(SQLConstants.NEWID);

            builder.HasMany(c => c.RefreshTokens)
               .WithOne(x => x.User)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CultureType)
               .WithMany()
               .HasForeignKey(x => x.CultureTypeId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            builder.OwnsOne(c => c.Address, AddressMapping.Configure);
            builder.MapBaseEntity();

            builder.ToTable(nameof(User), Schemas.CORE);
        }
    }
}
