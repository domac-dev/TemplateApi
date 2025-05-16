using App.Result;
using DomainEvent.Abstraction;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using Domain.Enumerations;

namespace Application.Modules.Authentication.Commands
{
    public record SignOutCommand : ICommand<Result>;

    internal class SignOutHandler(IRepository<ApplicationUser> repository, IAuthenticationManager authenticationManager, IUserManager userManager)
        : ICommandHandler<SignOutCommand, Result>
    {
        public async Task<Result> Handle(SignOutCommand request, CancellationToken cancellationToken)
        {
            IUserCredentials credentials = userManager.UserCredentials;
            ApplicationUser? user = await repository.GetByIdAsync(credentials.Id, cancellationToken);

            if (user is null)
                return Result.BadRequest("USER_DOESNT_EXIST");

            user.RefreshTokens.LastOrDefault()?.Revoke(TokenRevokeType.SignedOut.ToString());
            string refreshToken = authenticationManager.GetRefreshToken();

            authenticationManager.SignOut(refreshToken, credentials.Id);
            repository.Update(user);

            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success("LOGOUT_SUCCESS");
        }
    }
}
