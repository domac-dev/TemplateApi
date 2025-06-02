using App.Result;
using Application.Modules.Authentication.DTOs.Response;
using Ardalis.Specification;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using DomainEvent.Abstraction;

namespace Application.Modules.Authentication.Commands
{
    public record RefreshTokenCommand : ICommand<Result<RefreshTokenResponseDTO>>;

    internal class RefreshTokenHandler(IRepository<User> repository, IAuthenticationManager authenticationManager, IHttpContextService httpContextHelper)
        : ICommandHandler<RefreshTokenCommand, Result<RefreshTokenResponseDTO>>
    {
        public async Task<Result<RefreshTokenResponseDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string refreshToken = authenticationManager.GetRefreshToken();

            ISpecificationBuilder<User> query = new Specification<User>().Query
                .Include(x => x.RefreshTokens)
                .Where(ExpressionHelper.Valid<User>())
                .Where(c => c.RefreshTokens.Any(tok => tok.Token == refreshToken && tok.ExpiresAt > DateTime.UtcNow
                    && tok.ReasonRevoked == null));

            User? user = await repository.FirstOrDefaultAsync(query.Specification, cancellationToken);

            if (user is null)
                return Result.Unauthorized("UNATHORIZED");

            (IToken AccessToken, IToken RefreshToken) = authenticationManager.GenerateTokens(user);

            user.AddRefreshTokenAndReplace(RefreshToken.Value, RefreshToken.ExpiresAt, httpContextHelper.IPAddress());

            RefreshTokenResponseDTO response = AuthenticationMapper.RefreshTokenResponse(RefreshToken, AccessToken);

            repository.Update(user);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
