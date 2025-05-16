using System.Security.Claims;
using Domain.Abstraction;
using Domain.Abstraction.Security;

namespace Api.Common.Middleware
{
    public class AuthenticationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context, IHttpContextService httpContextHelper, IAuthenticationManager authenticationManager)
        {
            string bearer = httpContextHelper.BearerToken();

            if (!string.IsNullOrEmpty(bearer))
            {
                bool isAuthenticated = authenticationManager.Authenticate(bearer, out ClaimsPrincipal credentials);

                if (isAuthenticated)
                    context.User = credentials;
            }

            await _next(context);
        }
    }
}
