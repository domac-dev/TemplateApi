using App.Localization.Abstraction;
using MediatR;
using Application.Modules.Authentication.Commands;
using Application.Modules.Authentication.DTOs.Request;
using Domain.AccessControl;
using Api.Common.Attributes;
using Api.Common.MinimalAPI;

namespace Api.Endpoints
{
    public class AuthenticationModule(ILocalizator localizator) : EndpointModule(localizator)
    {
        protected override string BaseUrl => Routes.AUTH;
        public override void MapEndpoints(IEndpointRouteBuilder app)
        {
            RouteGroupBuilder routeBuilder = app.MapGroup(BaseUrl);

            routeBuilder.MapPost(string.Empty, async (SignInRequestDTO data, IMediator mediator) =>
            {
                return await Response(await mediator.Send(new SignInCommand(data)));

            }).AddEndpointFilter(new ValidationFilter<SignInRequestDTO>(_localizator)).AllowAnonymous();

            routeBuilder.MapPost("/reset-password", async (ResetPasswordRequestDTO request, IMediator mediator) =>
            {
                return await Response(await mediator.Send(new ResetPasswordCommand(request)));

            }).AllowAnonymous();

            routeBuilder.MapGet(string.Empty, async (IMediator mediator) =>
            {
                return await Response(await mediator.Send(new RefreshTokenCommand()));

            }).AllowAnonymous();


            routeBuilder.MapPut(string.Empty, async (IMediator mediator) =>
            {
                return await Response(await mediator.Send(new SignOutCommand()));

            }).AddEndpointFilter(new AuthorizationFilter(SecurityRole.USER));
        }
    }
}
