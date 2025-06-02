using Domain;
using FluentValidation;

namespace Application.Modules.Authentication.DTOs.Request;

public record SignInRequestDTO(string EmailAddress, string Password, bool RememberMe)
{
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
