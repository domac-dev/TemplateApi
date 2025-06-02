using Domain;
using Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Core
{
    public class CultureTypeConfiguration : IEntityTypeConfiguration<CultureType>
    {
        public void Configure(EntityTypeBuilder<CultureType> builder)
        {
            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Value)
                .HasMaxLength(ModelConstants.CultureModel.VALUE)
                .IsRequired();

            builder.Property(ct => ct.Type)
                .IsRequired();

            builder.Ignore(ct => ct.CultureInfo);

            builder.Ignore(x => x.Content);
            builder.HasMany(ct => ct.Translations)
                .WithOne()
                .HasForeignKey(ctt => ctt.CultureTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(ct => ct.Translations)
               .AutoInclude();

            builder.ToTable(nameof(CultureType), Schemas.CORE);

            builder.MapBaseEntity();
        }
    }
}
