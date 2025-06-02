using Domain;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public static class AddressMapping
    {
        public static void Configure<T>(OwnedNavigationBuilder<T, Address> addressBuilder) where T : class
        {
            addressBuilder.Property(a => a.Street)
                .HasColumnName(nameof(Address.Street))
                .IsRequired()
                .HasMaxLength(ModelConstants.AddressModel.STREET);

            addressBuilder.Property(a => a.Country)
                .HasColumnName(nameof(Address.Country))
                .IsRequired()
                .HasMaxLength(ModelConstants.AddressModel.COUNTRY);

            addressBuilder.Property(a => a.City)
                .HasColumnName(nameof(Address.City))
                .IsRequired()
                .HasMaxLength(ModelConstants.AddressModel.CITY);

            addressBuilder.Property(a => a.PostalCode)
                 .HasColumnName(nameof(Address.PostalCode))
                .HasMaxLength(ModelConstants.AddressModel.POSTAL_CODE);
        }
    }
}