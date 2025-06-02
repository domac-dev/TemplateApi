using Ardalis.GuardClauses;

namespace Domain.ValueObjects
{
    public class Address
    {
        public string Street { get; private set; } = null!;
        public string Country { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string? PostalCode { get; private set; }
        protected Address() { }
        public Address(string street, string country, string city, string? postalCode)
        {
            Street = Guard.Against.MinMaxLength(street, 1, ModelConstants.AddressModel.STREET);
            Country = Guard.Against.MinMaxLength(country, 1, ModelConstants.AddressModel.COUNTRY);
            City = Guard.Against.MinMaxLength(city, 1, ModelConstants.AddressModel.CITY);
            PostalCode = Guard.Against.MinMaxLengthNullable(postalCode, 5, ModelConstants.AddressModel.POSTAL_CODE);
        }
    }
}