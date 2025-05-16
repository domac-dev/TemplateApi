using App.Result;
using DomainEvent.Abstraction;
using Application.Modules.Authentication.DTOs.Request;
using Application.Modules.Authentication.DTOs.Response;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using Domain.Entities.Core.Specifications;

namespace Application.Modules.Authentication.Commands
{
    public record SignInCommand(SignInRequestDTO Data) : ICommand<Result<SignInResponseDTO>>;

    internal class SignInHandler(IRepository<ApplicationUser> repository, IHttpContextService httpContextHelper, IAuthenticationManager authenticationManager)
      : ICommandHandler<SignInCommand, Result<SignInResponseDTO>>
    {
        public async Task<Result<SignInResponseDTO>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await repository.FirstOrDefaultAsync(new UserByEmailSpec(request.Data.EmailAddress), cancellationToken);

            if (user is null)
                return Result.BadRequest("ERROR_INVALID_EMAIL");

            if (!BCrypt.Net.BCrypt.Verify(request.Data.Password, user.PasswordHash))
                return Result.BadRequest("ERROR_INVALID_PASSWORD");

            (IToken AccessToken, IToken RefreshToken) tokens = authenticationManager.GenerateTokens(user);

            authenticationManager.SetRefreshToken(tokens.RefreshToken);

            user.AddRefreshTokenAndRevokeLast(tokens.RefreshToken.Value, tokens.RefreshToken.ExpiresAt, httpContextHelper.IPAddress());

            SignInResponseDTO response = user.LoginResponse(tokens.AccessToken, tokens.RefreshToken);

            repository.Update(user);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(response);

        }
    }
}
