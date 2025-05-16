using App.Result;
using DomainEvent.Abstraction;
using Application.Modules.Authentication.DTOs.Request;
using Domain.Abstraction;
using Domain.Entities.Core;
using Domain.Entities.Core.Specifications;
using Domain.Enumerations;

namespace Application.Modules.Authentication.Commands
{
    public record RegisterCommand(RegisterRequestDTO Data) : ICommand<Result>;

    internal class RegisterHandler(IRepository<ApplicationUser> userRepository, IRepository<Role> roleRepository)
        : ICommandHandler<RegisterCommand, Result>
    {
        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            bool existingEmail = await userRepository.AnyAsync(new UserByEmailSpec(request.Data.EmailAddress), cancellationToken);

            if (existingEmail)
                return Result.BadRequest("USER_EXISTING_EMAIL");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Data.Password);
            ApplicationUser applicationUser = new(request.Data.EmailAddress, passwordHash, request.Data.FullName, request.Data.CultureType);

            Role? role = await roleRepository.FirstOrDefaultAsync(new RoleByTypeSpec(RoleType.Client), cancellationToken);

            if (role is null)
                return Result.BadRequest(string.Empty);

            applicationUser.AddRole(role);
            userRepository.Add(applicationUser);

            await userRepository.SaveChangesAsync(cancellationToken);
            return Result.Success("USER_CREATED_SUCCESS");
        }
    }
}
