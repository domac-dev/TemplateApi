using Domain;
using Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class HttpContextService(IHttpContextAccessor httpContextAccessor) : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private const string FORWARDED_HEADER = "X-Forwarded-For";
        public const string CULTURE = "X-Culture";
        public const string UNKNOWN = "Unknown";

        public string IPAddress()
        {
            return _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(FORWARDED_HEADER, out var value) ? value.ToString()
                : _httpContextAccessor.HttpContext.Connection?.RemoteIpAddress?.MapToIPv4().ToString() ?? UNKNOWN;
        }

        public string Culture()
        {
            if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(CULTURE, out var culture))
                return CultureConstants.CROATIAN;

            return culture.ToString();
        }

        public string BearerToken()
        {
            string authenticationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            string bearerHeader = "Bearer ";

            if (string.IsNullOrEmpty(authenticationHeader) || !authenticationHeader.StartsWith(bearerHeader, StringComparison.OrdinalIgnoreCase))
                return string.Empty;

            return authenticationHeader[bearerHeader.Length..].Trim();
        }
    }
}