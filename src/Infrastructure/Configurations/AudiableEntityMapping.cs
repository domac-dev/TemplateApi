using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public static class AudiableEntityMapping
    {
        public static void MapBaseEntity<T>(this EntityTypeBuilder<T> builder) where T : AudiableEntity
        {
            builder.Property(x => x.CreatedBy)
                .HasMaxLength(ModelConstants.GeneralModel.GUID)
                .HasDefaultValue(SQLConstants.SYSTEM);

            builder.Property(x => x.CreatedOn)
                .HasDefaultValueSql(SQLConstants.GETDATE);

            builder.Property(x => x.ModifiedBy)
                .HasMaxLength(ModelConstants.GeneralModel.GUID);

            builder.Property(x => x.ModifiedOn);

            builder.Property(x => x.ValidUntil);

        }
    }
}
