using App.Result;
using Application.Modules.Authentication.DTOs.Request;
using Ardalis.Specification;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using DomainEvent.Abstraction;

namespace Application.Modules.Authentication.Commands
{
    public record ResetPasswordCommand(ResetPasswordRequestDTO Data) : ICommand<Result>;

    internal class ResetPasswordHandler(IRepository<User> repository, IPasswordGenerator passwordGenerator)
    : ICommandHandler<ResetPasswordCommand, Result>
    {
        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            ISpecificationBuilder<User> query = new Specification<User>().Query
                .Where(ExpressionHelper.Valid<User>())
                .Where(c => c.Email == request.Data.EmailAddress && c.EmailConfirmed);

            User? user = await repository.FirstOrDefaultAsync(query.Specification, cancellationToken);

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
