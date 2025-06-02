using App.Result;
using Application.Modules.Authentication.DTOs.Request;
using Application.Modules.Authentication.DTOs.Response;
using Ardalis.Specification;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using DomainEvent.Abstraction;

namespace Application.Modules.Authentication.Commands
{
    public record SignInCommand(SignInRequestDTO Data) : ICommand<Result<SignInResponseDTO>>;

    internal class SignInHandler(IRepository<User> repository, IHttpContextService httpContextHelper, IAuthenticationManager authenticationManager)
      : ICommandHandler<SignInCommand, Result<SignInResponseDTO>>
    {
        public async Task<Result<SignInResponseDTO>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            ISpecificationBuilder<User> query = new Specification<User>().Query
              .Include(x => x.CultureType)
              .Include(x => x.Roles)
              .Include(x => x.Claims)
              .Include(x => x.RefreshTokens)
              .Where(ExpressionHelper.Valid<User>())
              .Where(c => c.Email == request.Data.EmailAddress);

            User? user = await repository.FirstOrDefaultAsync(query.Specification, cancellationToken);

            if (user is null)
                return Result.BadRequest("ERROR_INVALID_EMAIL");

            else if (!user.EmailConfirmed)
                return Result.BadRequest("ERROR_EMAIL_NOT_CONFIRMED");

            else if (!BCrypt.Net.BCrypt.Verify(request.Data.Password, user.PasswordHash))
                return Result.BadRequest("ERROR_INVALID_PASSWORD");

            (IToken AccessToken, IToken RefreshToken) = authenticationManager.GenerateTokens(user);

            authenticationManager.SetRefreshToken(RefreshToken);

            user.AddRefreshTokenAndRevokeLast(RefreshToken.Value, RefreshToken.ExpiresAt, httpContextHelper.IPAddress());

            SignInResponseDTO response = user.LoginResponse(AccessToken, RefreshToken);

            repository.Update(user);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(response);

        }
    }
}
