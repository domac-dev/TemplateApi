using App.Localization.Abstraction;
using App.Result;
using System.Globalization;

namespace Api.Common.MinimalAPI
{
    public abstract class EndpointModule(ILocalizator localizator)
    {
        protected readonly ILocalizator _localizator = localizator;
        protected abstract string BaseUrl { get; }
        public abstract void MapEndpoints(IEndpointRouteBuilder app);
        protected virtual async Task<Microsoft.AspNetCore.Http.IResult> Response<T>(Result<T> result)
        {
            if (!string.IsNullOrEmpty(result.Message))
                result.Message = await _localizator.GetAsync(result.Message, CultureInfo.CurrentCulture.Name);

            if (result.IsSuccess)
                return Results.Ok(result);

            return result.StatusCode switch
            {
                ResultStatus.NoContent => Results.NoContent(),
                ResultStatus.Unauthorized => Results.Unauthorized(),
                ResultStatus.InternalError => Results.Problem(result.Message),
                ResultStatus.Forbidden => Results.Forbid(),
                ResultStatus.BadRequest => Results.BadRequest(result),
                _ => Results.Problem(result.Message)
            };
        }
    }

}
