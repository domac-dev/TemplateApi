using App.Result;
using DomainEvent.Abstraction;
using Application.Modules.Authentication.DTOs.Response;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using Domain.Entities.Core.Specifications;

namespace Application.Modules.Authentication.Commands
{
    public record RefreshTokenCommand : ICommand<Result<RefreshTokenResponseDTO>>;

    internal class RefreshTokenHandler(IRepository<ApplicationUser> repository, IAuthenticationManager authenticationManager, IHttpContextService httpContextHelper)
        : ICommandHandler<RefreshTokenCommand, Result<RefreshTokenResponseDTO>>
    {
        public async Task<Result<RefreshTokenResponseDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await repository.FirstOrDefaultAsync(
                new UserByRefreshTokenSpec(authenticationManager.GetRefreshToken()), cancellationToken);

            if (user is null)
                return Result.Unauthorized("UNATHORIZED");

            (IToken AccessToken, IToken RefreshToken) tokens = authenticationManager.GenerateTokens(user);

            user.AddRefreshTokenAndReplace(tokens.RefreshToken.Value, tokens.RefreshToken.ExpiresAt, httpContextHelper.IPAddress());

            RefreshTokenResponseDTO response = AuthenticationMapper.RefreshTokenResponse(tokens.RefreshToken, tokens.AccessToken);

            repository.Update(user);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
