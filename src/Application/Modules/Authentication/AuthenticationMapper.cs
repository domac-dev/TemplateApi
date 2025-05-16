using Application.Modules.Authentication.DTOs;
using Application.Modules.Authentication.DTOs.Response;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using Domain.Enumerations;

namespace Application.Modules.Authentication
{
    internal static class AuthenticationMapper
    {
        public static TokenDTO MapToTokenDTO(this IToken token) => new(token.Value, token.ExpiresAt);
        public static SignInResponseDTO LoginResponse(this ApplicationUser user, IToken accessToken, IToken refreshToken) => new()
        {
            Id = user.Id,
            Username = user.UserName,
            FullName = user.FullName,
            Email = user.Email,
            Roles = user.Roles.Select(role => role.Name).ToList(),
            AccessToken = accessToken.MapToTokenDTO(),
            RefreshToken = refreshToken.MapToTokenDTO(),
            Culture = CultureExtensions.AsString(user.Culture),
        };

        public static RefreshTokenResponseDTO RefreshTokenResponse(IToken refreshToken, IToken accessToken) => new()
        {
            AccessToken = accessToken.MapToTokenDTO(),
            RefreshToken = refreshToken.MapToTokenDTO()
        };
    }
}
