using Domain;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Infrastructure.Security.Models;
using Jitbit.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Security
{
    public class AuthenticationManager(AppSettings appSettings, IHttpContextAccessor httpContextAccessor) : IAuthenticationManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly AuthenticationConfig _authenticationConfig = appSettings.AuthenticationConfig!;
        private readonly FastCache<string, string> _blacklistedTokens = new();

        private const string COOKIE_KEY = "refresh-token";
        private const string DEFAULT_DOMAIN = "localhost";

        public bool Authenticate(string token, out ClaimsPrincipal credentials)
        {
            credentials = new ClaimsPrincipal();

            if (_blacklistedTokens.TryGet(token, out _))
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_authenticationConfig.Secret);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = _authenticationConfig.ValidateIssuer,
                    ValidateAudience = _authenticationConfig.ValidateAudience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _authenticationConfig.Issuer,
                    ValidAudience = _authenticationConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                ClaimsPrincipal? principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken &&
                    jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    credentials = principal;
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public void SignOut(string token, int userId)
        {
            _blacklistedTokens.AddOrUpdate(token, userId.ToString(), TimeSpan.FromMinutes(_authenticationConfig.AccessTokenExpiration));
        }

        public (IToken AccessToken, IToken RefreshToken) GenerateTokens(IUserCredentials credentials)
        {
            return (GenerateAccessToken(credentials), GenerateRefreshToken());
        }

        public void SetRefreshToken(IToken refreshToken)
        {
            string domain = appSettings.Domain;

            var cookieOptions = new CookieOptions
            {
                Domain = string.IsNullOrEmpty(domain) ? DEFAULT_DOMAIN : domain,
                Path = "/",
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Expires = refreshToken.ExpiresAt
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(COOKIE_KEY, refreshToken.Value, cookieOptions);
        }

        public string GetRefreshToken()
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies[COOKIE_KEY];

            if (string.IsNullOrEmpty(cookie))
                throw new UnauthorizedAccessException("Token cookie is null.");

            return cookie;
        }

        private Token GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);

            string refreshToken = Convert.ToBase64String(randomNumber);

            return new Token(refreshToken, DateTime.UtcNow.AddDays(_authenticationConfig.RefreshTokenExpiration));
        }

        private Token GenerateAccessToken(IUserCredentials user)
        {
            var claims = PrincipalHelper.GenerateClaims(user);

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_authenticationConfig.Secret));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new(
              issuer: _authenticationConfig.Issuer,
              audience: _authenticationConfig.Audience,
              claims: claims,
              expires: DateTime.UtcNow.AddMinutes(_authenticationConfig.AccessTokenExpiration),
              signingCredentials: creds
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new Token(token, DateTime.UtcNow.AddMinutes(_authenticationConfig.AccessTokenExpiration));
        }
    }
}
