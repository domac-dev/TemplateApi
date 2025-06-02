using Application.Core.DTOs;
using Domain;
using Domain.Enumerations;
using FluentValidation;

namespace Application.Modules.Authentication.DTOs.Request
{
    public class RegisterRequestDTO
    {
        public string EmailAddress { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? UserName { get; set; } = null!;
        public CultureTypeEnum CultureType { get; set; }
        public AddressDTO Address { get; set; } = null!;

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

                RuleFor(x => x.UserName)
                    .NotEmpty().WithMessage("REQUIRED_FIELD")
                    .MaximumLength(ModelConstants.UserModel.USERNAME);

                RuleFor(x => x.CultureType)
                    .IsInEnum().WithMessage("FORMAT_ERROR");

                RuleFor(x => x.Address)
                    .NotNull().WithMessage("REQUIRED_FIELD")
                    .SetValidator(new AddressDTO.Validator());
            }
        }
    }
}
