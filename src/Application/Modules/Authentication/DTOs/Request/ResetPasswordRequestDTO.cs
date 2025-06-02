using Domain;
using FluentValidation;

namespace Application.Modules.Authentication.DTOs.Request;

public record ResetPasswordRequestDTO(string EmailAddress)
{
    public class Validator : AbstractValidator<ResetPasswordRequestDTO>
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
