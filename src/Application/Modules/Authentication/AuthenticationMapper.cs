using Application.Modules.Authentication.DTOs;
using Application.Modules.Authentication.DTOs.Response;
using Domain.Abstraction.Security;
using Domain.Entities.Core;

namespace Application.Modules.Authentication
{
    internal static class AuthenticationMapper
    {
        public static TokenDTO MapToTokenDTO(this IToken token) => new(token.Value, token.ExpiresAt);
        public static SignInResponseDTO LoginResponse(this User user, IToken accessToken, IToken refreshToken) => new()
        {
            Id = user.Id,
            Telephone = user.Telephone,
            FullName = user.FullName,
            Email = user.Email,
            Roles = [.. user.Roles.Select(role => role.Name)],
            Claims = [.. user.Claims.Select(claim => claim.Name)],
            AccessToken = accessToken.MapToTokenDTO(),
            RefreshToken = refreshToken.MapToTokenDTO(),
            Culture = user.Culture,
        };

        public static RefreshTokenResponseDTO RefreshTokenResponse(IToken refreshToken, IToken accessToken) => new()
        {
            AccessToken = accessToken.MapToTokenDTO(),
            RefreshToken = refreshToken.MapToTokenDTO()
        };
    }
}
