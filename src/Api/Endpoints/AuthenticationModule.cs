using Api.Common.Attributes;
using Api.Common.MinimalAPI;
using App.Localization.Abstraction;
using Application.Modules.Authentication.Commands;
using Application.Modules.Authentication.DTOs.Request;
using Domain.AccessControl;
using MediatR;

namespace Api.Endpoints
{
    public class AuthenticationModule(ILocalizator localizator) : EndpointModule(localizator)
    {
        protected override string BaseUrl => Routes.AUTH;
        public override void MapEndpoints(IEndpointRouteBuilder app)
        {
            RouteGroupBuilder routeBuilder = app.MapGroup(BaseUrl);

            //POST api/auth
            routeBuilder.MapPost(string.Empty, async (SignInRequestDTO data, IMediator mediator) =>
            {
                return await Response(await mediator.Send(new SignInCommand(data)));

            }).AddEndpointFilter(new ValidationFilter<SignInRequestDTO>(_localizator)).AllowAnonymous();

            //POST api/auth/reset-password
            routeBuilder.MapPost("/reset-password", async (ResetPasswordRequestDTO request, IMediator mediator) =>
            {
                return await Response(await mediator.Send(new ResetPasswordCommand(request)));

            }).AllowAnonymous();

            //GET api/auth
            routeBuilder.MapGet(string.Empty, async (IMediator mediator) =>
            {
                return await Response(await mediator.Send(new RefreshTokenCommand()));

            }).AllowAnonymous();

            //PUT api/auth
            routeBuilder.MapPut(string.Empty, async (IMediator mediator) =>
            {
                return await Response(await mediator.Send(new SignOutCommand()));

            }).AddEndpointFilter(new AuthorizationFilter(SecurityRole.USER));

            //PUT api/auth{confirmationToken}
            routeBuilder.MapPut("/{confirmationToken}", async (string confirmationToken, IMediator mediator) =>
            {
                return await Response(await mediator.Send(new ConfirmEmailCommand(confirmationToken)));
            }).AllowAnonymous();
        }
    }
}
