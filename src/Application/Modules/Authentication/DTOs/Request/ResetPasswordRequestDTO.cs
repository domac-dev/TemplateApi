using FluentValidation;
using Domain;

namespace Application.Modules.Authentication.DTOs.Request
{
    public class ResetPasswordRequestDTO
    {
        public string EmailAddress { get; set; } = null!;
        public class Validator : AbstractValidator<SignInRequestDTO>
        {
            public Validator()
            {
                RuleFor(x => x.EmailAddress)
                     .NotEmpty().WithMessage("REQUIRED_FIELD")
                     .MaximumLength(ModelConstants.GeneralModel.MAX)
                     .EmailAddress().WithMessage("FORMAT_ERROR");
            }
        }
    }
}
