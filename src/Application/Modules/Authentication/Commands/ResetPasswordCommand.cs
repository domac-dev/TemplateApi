using App.Result;
using Application.Modules.Authentication.DTOs.Request;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using Domain.Entities.Core.Specifications;
using DomainEvent.Abstraction;

namespace Application.Modules.Authentication.Commands
{
    public record ResetPasswordCommand(ResetPasswordRequestDTO Data) : ICommand<Result>;

    internal class ResetPasswordHandler(IRepository<ApplicationUser> repository, IPasswordGenerator passwordGenerator)
    : ICommandHandler<ResetPasswordCommand, Result>
    {
        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await repository.FirstOrDefaultAsync(new UserByEmailSpec(request.Data.EmailAddress), cancellationToken);

            if (user is null)
                return Result.BadRequest("EMAIL_DOESNT_EXIST");

            string generatedPassword = passwordGenerator.Generate();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(generatedPassword);

            user.PasswordHash = passwordHash;

            repository.Update(user);
            //TODO - Send email with new password!

            await repository.SaveChangesAsync(cancellationToken);
            return Result.Success("PASSWORD_RESET_SUCCESS");
        }
    }
}
