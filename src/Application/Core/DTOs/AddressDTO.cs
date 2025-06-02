using Domain;
using FluentValidation;

namespace Application.Core.DTOs
{
    public record AddressDTO(string Street, string Country, string City, string? PostalCode)
    {
        public class Validator : AbstractValidator<AddressDTO>
        {
            public Validator()
            {
                RuleFor(x => x.Street)
                    .NotEmpty().WithMessage("REQUIRED_FIELD")
                    .MinimumLength(1)
                    .MaximumLength(ModelConstants.AddressModel.STREET);

                RuleFor(x => x.Country)
                    .NotEmpty().WithMessage("REQUIRED_FIELD")
                    .MinimumLength(1)
                    .MaximumLength(ModelConstants.AddressModel.COUNTRY);

                RuleFor(x => x.City)
                    .NotEmpty().WithMessage("REQUIRED_FIELD")
                    .MinimumLength(1)
                    .MaximumLength(ModelConstants.AddressModel.CITY);

                RuleFor(x => x.PostalCode)
                    .MinimumLength(5).When(x => !string.IsNullOrWhiteSpace(x.PostalCode))
                    .MaximumLength(ModelConstants.AddressModel.POSTAL_CODE)
                    .When(x => !string.IsNullOrWhiteSpace(x.PostalCode));
            }
        }
    }


}
