using Domain.Abstraction;
using Domain.Enumerations;
using Domain.Exceptions;
using Infrastructure.Security.Models;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public static class PrincipalHelper
    {
        public static readonly string CUSTOM_CLAIM = "CustomClaim";

        public static List<Claim> GenerateClaims(IUserCredentials user)
        {
            if (user is null) throw new NotAuthorizedException();

            List<Claim> claims = [
                new(ClaimTypes.NameIdentifier, user.Id!.ToString()!),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Locality, user.Culture.ToString())
            ];

            claims.AddRange(user.RolesAsString.Split(',').Select(role => new Claim(ClaimTypes.Role, role.Trim())));
            claims.AddRange(user.ClaimsAsString.Split(',').Select(claim => new Claim(CUSTOM_CLAIM, claim.Trim())));

            return claims;
        }

        public static IUserCredentials ToUserCredentials(ClaimsPrincipal principal)
        {
            if (principal is null) throw new NotAuthorizedException();

            IEnumerable<Claim> claims = principal.Claims;

            long identifier = long.Parse((claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier) ?? throw new NotAuthorizedException()).Value);

            string email = (claims.FirstOrDefault(c => c.Type == ClaimTypes.Email) ?? throw new NotAuthorizedException()).Value;

            string rolesString = string.Join(",", claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value));
            string claimsString = string.Join(",", claims.Where(c => c.Type == CUSTOM_CLAIM).Select(c => c.Value));

            CultureType culture = Enum.TryParse<CultureType>(claims.FirstOrDefault(c => c.Type == ClaimTypes.Locality)?.Value, out var parsedCulture)
                ? parsedCulture : CultureType.Croatian;

            return new UserCredentials(identifier, email, rolesString, claimsString, culture);
        }
    }
}
