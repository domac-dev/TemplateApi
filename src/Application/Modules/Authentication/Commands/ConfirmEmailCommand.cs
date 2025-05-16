using App.Result;
using Application.Modules.Authentication.DTOs.Response;
using Domain.Abstraction;
using Domain.Abstraction.Security;
using Domain.Entities.Core;
using Domain.Entities.Core.Specifications;
using DomainEvent.Abstraction;

namespace Application.Modules.Authentication.Commands
{
    public record ConfirmEmailCommand(string ConfirmationToken) : ICommand<Result>;

    internal class ConfirmEmailHandler(IRepository<ApplicationUser> repository): ICommandHandler<ConfirmEmailCommand, Result>
    {
        public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? user = await repository.FirstOrDefaultAsync(
                new UserByConfirmationTokenSpec(request.ConfirmationToken), cancellationToken);

            if (user is null)
                return Result.BadRequest("ERROR_CONFIRMATION_TOKEN");

            user.EmailConfirmed = true;

            repository.Update(user);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.NoContent();
        }
    }
}
