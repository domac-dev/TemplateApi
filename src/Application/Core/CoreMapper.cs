using Application.Core.DTOs;
using Domain.ValueObjects;

namespace Application.Core
{
    internal static class CoreMapper
    {
        internal static AddressDTO ToDTO(this Address address)
        {
            return new AddressDTO(
                Street: address.Street,
                Country: address.Country,
                City: address.City,
                PostalCode: address.PostalCode
            );
        }
        internal static Address ToDomain(this AddressDTO dto)
        {
            return new Address(
                street: dto.Street,
                country: dto.Country,
                city: dto.City,
                postalCode: dto.PostalCode
            );
        }
    }
}
