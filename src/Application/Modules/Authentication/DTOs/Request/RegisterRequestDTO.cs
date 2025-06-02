using Application.Core.DTOs;
using Domain;
using Domain.Enumerations;
using FluentValidation;

namespace Application.Modules.Authentication.DTOs.Request;

public record RegisterRequestDTO(string EmailAddress, string Telephone, string Password, string FullName,
    CultureTypeEnum CultureType, AddressDTO Address)
{
    public class Validator : AbstractValidator<RegisterRequestDTO>
    {
        public Validator()
        {
            RuleFor(x => x.Telephone)
               .NotEmpty().WithMessage("REQUIRED_FIELD")
               .Matches(@"^\+?[0-9\s\-]{7,15}$").WithMessage("FORMAT_ERROR")
               .MaximumLength(ModelConstants.GeneralModel.TELEPHONE);

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("REQUIRED_FIELD")
                .MaximumLength(ModelConstants.GeneralModel.MAX)
                .EmailAddress().WithMessage("FORMAT_ERROR");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("REQUIRED_FIELD")
                .MinimumLength(6)
                .MaximumLength(ModelConstants.GeneralModel.MAX);

            RuleFor(x => x.CultureType)
                .IsInEnum().WithMessage("FORMAT_ERROR");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("REQUIRED_FIELD")
                .SetValidator(new AddressDTO.Validator());
        }
    }
}
