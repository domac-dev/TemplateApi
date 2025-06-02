using System.Security.Claims;

namespace Domain.Abstraction.Security
{
    public interface IAuthenticationManager
    {
        bool Authenticate(string token, out ClaimsPrincipal credentials);
        void SignOut(string token, int userId);
        (IToken AccessToken, IToken RefreshToken) GenerateTokens(IUserCredentials credentials);
        void SetRefreshToken(IToken refreshToken);
        string GetRefreshToken();
    }

    public interface IToken
    {
        DateTime ExpiresAt { get; }
        string Value { get; }
    }
}
