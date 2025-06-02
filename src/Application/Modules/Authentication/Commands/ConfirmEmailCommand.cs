using App.Result;
using Ardalis.Specification;
using Domain.Abstraction;
using Domain.Entities.Core;
using DomainEvent.Abstraction;

namespace Application.Modules.Authentication.Commands
{
    public record ConfirmEmailCommand(string ConfirmationToken) : ICommand<Result>;

    internal class ConfirmEmailHandler(IRepository<User> repository) : ICommandHandler<ConfirmEmailCommand, Result>
    {
        public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            ISpecificationBuilder<User> query = new Specification<User>().Query
                .Where(ExpressionHelper.Valid<User>())
                .Where(c => c.EmailConfirmationToken == request.ConfirmationToken && !c.EmailConfirmed);

            User? user = await repository.FirstOrDefaultAsync(query.Specification, cancellationToken);

            if (user is null)
                return Result.BadRequest("ERROR_CONFIRMATION_TOKEN");

            user.EmailConfirmed = true;

            repository.Update(user);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.NoContent();
        }
    }
}
