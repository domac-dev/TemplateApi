using FluentValidation;
using Domain;
using Domain.Enumerations;

namespace Application.Modules.Authentication.DTOs.Request
{
    public class RegisterRequestDTO
    {
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? UserName { get; set; } = null!;
        public CultureType CultureType { get; set; }

        public class Validator : AbstractValidator<RegisterRequestDTO>
        {
            public Validator()
            {
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
            }
        }
    }
}
