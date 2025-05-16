using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Infrastructure.Security
{
    public class UserManager : IUserManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IUserCredentials UserCredentials { get; private set; } = default!;
        public UserManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserCredentials = InitUser();
        }

        public bool IsInRole(string role)
        {
            return UserCredentials.RolesAsString.Split(',')
                .Select(r => r.Trim())
                .Contains(role.Trim(), StringComparer.OrdinalIgnoreCase);
        }

        public bool HasClaim(string claim)
        {
            return UserCredentials.ClaimsAsString.Split(',')
              .Select(r => r.Trim())
              .Contains(claim.Trim(), StringComparer.OrdinalIgnoreCase);
        }

        private IUserCredentials InitUser()
        {
            ClaimsPrincipal? principal = _httpContextAccessor.HttpContext?.User;
            IIdentity? identity = _httpContextAccessor.HttpContext?.User?.Identity;

            return identity == null || !identity.IsAuthenticated
                ? throw new NotAuthorizedException() : PrincipalHelper.ToUserCredentials(principal!);
        }
    }
}
