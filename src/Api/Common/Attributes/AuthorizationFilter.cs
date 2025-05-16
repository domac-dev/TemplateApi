using App.Result;
using Domain.Abstraction.Security;

namespace Api.Common.Attributes
{
    public class AuthorizationFilter(string? roles = null, string? claims = null) : IEndpointFilter
    {
        public string[] Roles { get; set; } = roles == null ? [] : roles.Split(",");
        public string[] Claims { get; set; } = claims == null ? [] : claims.Split(",");

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            IUserManager userManager = context.HttpContext.RequestServices.GetRequiredService<IUserManager>();

            if (Roles.Length != 0 && !Roles.Any(userManager.IsInRole))
                return Forbidden(context);

            if (Claims.Length != 0 && !Claims.Any(userManager.HasClaim))
                return Forbidden(context);

            return await next(context);
        }

        private static Result Forbidden(EndpointFilterInvocationContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Result.Forbidden();
        }
    }
}
