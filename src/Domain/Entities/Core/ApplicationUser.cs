using Ardalis.GuardClauses;
using System.Data;
using Domain.Abstraction;
using Domain.Enumerations;

namespace Domain.Entities.Core
{
    public class ApplicationUser : AudiableEntity, IAggregateRoot, IUserCredentials
    {
        public long Id { get; set; }
        public string EmailConfirmationToken { get; private set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string? UserName { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public CultureType Culture { get; set; } = default!;

        private readonly List<RefreshToken> _refreshTokens = [];
        private readonly List<Claim> _claims = [];
        private readonly List<Role> _roles = [];

        protected ApplicationUser() { }
        public ApplicationUser(string email, string passwordHash, string fullName, CultureType culture, string? username = null)
        {
            Email = Guard.Against.InvalidEmailFormat(email);
            FullName = Guard.Against.MinMaxLength(fullName, 1, ModelConstants.GeneralModel.MAX);
            EmailConfirmed = false;
            EmailConfirmationToken = Guid.NewGuid().ToString();
            PasswordHash = Guard.Against.NullOrEmpty(passwordHash);
            Culture = Guard.Against.EnumOutOfRange(culture);
            UserName = Guard.Against.MinMaxLengthNullable(username, 1, ModelConstants.UserModel.USERNAME);
        }

        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
        public IReadOnlyCollection<Claim> Claims => _claims;
        public IReadOnlyCollection<Role> Roles => _roles;

        public string RolesAsString => string.Join(",", _roles.Select(x => x.Name));
        public string ClaimsAsString => string.Join(",", _claims.Select(x => x.Name));

        public void AddClaim(Claim claim) => _claims.Add(claim);
        public void RemoveClaim(Claim claim) => _claims.Remove(claim);

        public void AddRole(Role role) => _roles.Add(role);
        public void RemoveRole(Role role) => _roles.Remove(role);

        public void AddRefreshTokenAndRevokeLast(string token, DateTime d, string? createdByIp)
        {
            _refreshTokens.LastOrDefault()?.Revoke(TokenRevokeType.Replaced.ToString());
            _refreshTokens.Add(new RefreshToken(token, d, createdByIp));
        }

        public void AddRefreshTokenAndReplace(string token, DateTime d, string? createdByIp)
        {
            _refreshTokens.LastOrDefault()?.ReplaceToken(token);
            _refreshTokens.Add(new RefreshToken(token, d, createdByIp));
        }
    }
}
