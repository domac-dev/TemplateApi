using App.Localization.Abstraction;
using App.Result;
using FluentValidation;

namespace Api.Common.Attributes
{
    public class ValidationFilter<T>(ILocalizator localizator) : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            T? argToValidate = context.GetArgument<T>(0);
            IValidator<T>? validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            if (validator is not null)
            {
                var validationResults = await validator.ValidateAsync(argToValidate!);

                var errorList = new Dictionary<string, KeyValuePair<string, string>[]>();

                foreach (var validationResult in validationResults.Errors)
                {
                    var propertyName = validationResult.PropertyName ?? string.Empty; //toCamelCase TODO

                    var error = new KeyValuePair<string, string>(propertyName, validationResult?.ErrorMessage ?? string.Empty);

                    if (!errorList.TryGetValue(propertyName, out KeyValuePair<string, string>[]? value))
                        errorList.Add(propertyName, [error]);

                    else
                        errorList[propertyName] = [.. value, error];

                }

                if (errorList.Count > 0)
                {
                    var result = Result.BadRequest(errorList.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(e => localizator[e.Value]).ToArray()));
                    return Results.BadRequest(result);
                }
            }

            return await next.Invoke(context);
        }
    }
}
