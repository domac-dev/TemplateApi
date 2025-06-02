using App.Result;
using Application.Core;
using Application.Modules.Authentication.DTOs.Request;
using Ardalis.Specification;
using Domain.Abstraction;
using Domain.Entities.Core;
using Domain.Entities.Security;
using Domain.Enumerations;
using DomainEvent.Abstraction;

namespace Application.Modules.Authentication.Commands
{
    public record RegisterCommand(RegisterRequestDTO Data) : ICommand<Result>;

    internal class RegisterHandler(IRepository<User> userRepository, IReadRepository<Role> roleRepository,
        IReadRepository<CultureType> cultureRepository) : ICommandHandler<RegisterCommand, Result>
    {
        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            ISpecificationBuilder<User> query = new Specification<User>().Query
                .Where(ExpressionHelper.Valid<User>())
                .Where(c => c.Email == request.Data.EmailAddress);

            bool existingEmail = await userRepository.AnyAsync(query.Specification, cancellationToken);

            if (existingEmail)
                return Result.BadRequest("USER_EXISTING_EMAIL");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Data.Password);

            ISpecificationBuilder<CultureType> cultureQuery = new Specification<CultureType>().Query
                .Where(ExpressionHelper.Valid<CultureType>())
                .Where(c => c.Type == request.Data.CultureType);

            CultureType? cultureType = await cultureRepository.FirstOrDefaultAsync(cultureQuery.Specification, cancellationToken);

            if (cultureType is null)
                return Result.BadRequest("CULTURE_DOES_NOT_EXIST");

            User applicationUser = new(request.Data.EmailAddress, passwordHash, request.Data.FullName, cultureType.Id,
                request.Data.Telephone, request.Data.Address.ToDomain());

            ISpecificationBuilder<Role> roleQuery = new Specification<Role>().Query
                .Where(ExpressionHelper.Valid<Role>())
                .Where(c => c.Type == RoleTypeEnum.Client);

            Role? role = await roleRepository.FirstOrDefaultAsync(roleQuery.Specification, cancellationToken);

            if (role is null)
                return Result.BadRequest("ROLE_DOES_NOT_EXIST");

            applicationUser.AddRole(role);
            userRepository.Add(applicationUser);

            await userRepository.SaveChangesAsync(cancellationToken);
            return Result.Success("USER_CREATED_SUCCESS");
        }
    }
}
