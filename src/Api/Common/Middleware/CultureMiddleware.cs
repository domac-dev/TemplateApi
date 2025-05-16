using Domain.Abstraction;
using System.Globalization;

namespace Api.Common.Middleware
{
    public class CultureMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IHttpContextService httpContextHelper)
        {
            string culture = httpContextHelper.Culture();
            CultureInfo cultureInfo = new(culture);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
