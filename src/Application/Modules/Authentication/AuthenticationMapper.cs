using Application.Modules.Authentication.DTOs;
using Application.Modules.Authentication.DTOs.Response;
using Domain.Abstraction.Security;
using Domain.Entities.Core;

namespace Application.Modules.Authentication
{
    internal static class AuthenticationMapper
    {
        public static TokenDTO MapToTokenDTO(this IToken token) => new(token.Value, token.ExpiresAt);
        public static SignInResponseDTO LoginResponse(this User user, IToken accessToken, IToken refreshToken) =>
        new(
            Id: user.Id,
            FullName: user.FullName,
            Telephone: user.Telephone,
            Email: user.Email,
            Culture: user.Culture,
            Claims: user.Claims.Select(c => c.Name).ToList(),
            Roles: user.Roles.Select(r => r.Name).ToList(),
            AccessToken: accessToken.MapToTokenDTO(),
            RefreshToken: refreshToken.MapToTokenDTO()
        );

        public static RefreshTokenResponseDTO RefreshTokenResponse(IToken refreshToken, IToken accessToken) =>
        new(
            RefreshToken: refreshToken.MapToTokenDTO(),
            AccessToken: accessToken.MapToTokenDTO()
        );
    }
}
