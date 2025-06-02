using App.Result;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using Domain.Enumerations;
using DomainEvent.Abstraction;

namespace Application.Modules.Authentication.Commands
{
    public record SignOutCommand : ICommand<Result>;

    internal class SignOutHandler(IRepository<User> repository, IAuthenticationManager authenticationManager, IUserManager userManager)
        : ICommandHandler<SignOutCommand, Result>
    {
        public async Task<Result> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            IUserCredentials credentials = userManager.UserCredentials;
            User? user = await repository.GetByIdAsync(credentials.Id, cancellationToken);

            if (user is null)
                return Result.BadRequest("USER_DOESNT_EXIST");

            user.RefreshTokens.LastOrDefault()?.Revoke(TokenRevokeTypeEnum.SignedOut.ToString());
            string refreshToken = authenticationManager.GetRefreshToken();

            authenticationManager.SignOut(refreshToken, credentials.Id);
            repository.Update(user);

            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success("LOGOUT_SUCCESS");
        }
    }
}
