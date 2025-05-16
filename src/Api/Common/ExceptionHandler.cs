using App.Result;
using Domain.Exceptions;

namespace Api.Common
{
    public static class ExceptionHandler
    {
        public delegate Task HandleExceptionDelegate(HttpContext context, Exception exception);

        public static async Task HandleExceptionAsync(HttpContext context, Exception? exception, ILogger logger)
        {
            var result = exception switch
            {
                NotAuthorizedException => Result.Unauthorized(),
                Domain.Exceptions.ApplicationException => Result.BadRequest(exception.Message),
                _ => Result.InternalError(exception?.Message ?? string.Empty)
            };

            if (exception is not NotAuthorizedException && exception is not Domain.Exceptions.ApplicationException)
                logger.LogError(exception, "An unexpected error occurred: {Message}", exception?.Message ?? "Unknown error");

            context.Response.StatusCode = (int)result.StatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
