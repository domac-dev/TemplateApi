using Domain;
using FluentValidation;

namespace Application.Modules.Authentication.DTOs.Request
{
    public class SignInRequestDTO
    {
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }

        public class Validator : AbstractValidator<SignInRequestDTO>
        {
            public Validator()
            {
                RuleFor(x => x.EmailAddress)
                     .NotEmpty().WithMessage("REQUIRED_FIELD")
                     .MaximumLength(ModelConstants.GeneralModel.MAX)
                     .EmailAddress().WithMessage("FORMAT_ERROR");

                RuleFor(x => x.Password)
                     .NotEmpty().WithMessage("REQUIRED_FIELD");
            }
        }
    }
}
