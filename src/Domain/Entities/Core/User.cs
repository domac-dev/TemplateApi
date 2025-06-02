using Ardalis.GuardClauses;
using Domain.Abstraction;
using Domain.Entities.Security;
using Domain.Enumerations;
using Domain.Extensions;
using Domain.ValueObjects;
using System.Data;

namespace Domain.Entities.Core
{
    public class User : AudiableEntity, IAggregateRoot, IUserCredentials
    {
        public int Id { get; set; }
        public string EmailConfirmationToken { get; private set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public CultureType CultureType { get; set; } = null!;
        public int CultureTypeId { get; set; }
        public Address Address { get; set; } = null!;

        private readonly List<RefreshToken> _refreshTokens = [];
        private readonly List<Claim> _claims = [];
        private readonly List<Role> _roles = [];

        protected User() { }
        public User(string email, string passwordHash, string fullName, int cultureType, string telephone, Address address)
        {
            Email = Guard.Against.InvalidEmailFormat(email);
            FullName = Guard.Against.MinMaxLength(fullName, 1, ModelConstants.GeneralModel.MAX);
            EmailConfirmed = false;
            EmailConfirmationToken = Guid.NewGuid().ToString();
            PasswordHash = Guard.Against.NullOrEmpty(passwordHash);
            CultureTypeId = Guard.Against.NegativeOrZero(cultureType);
            Telephone = Guard.Against.Telephone(telephone);
            Address = Guard.Against.Null(address);
        }

        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
        public IReadOnlyCollection<Claim> Claims => _claims;
        public IReadOnlyCollection<Role> Roles => _roles;

        public string Culture { get => CultureType?.Value ?? CultureConstants.CROATIAN; }
        public string RolesAsString => string.Join(",", _roles.Select(x => x.Name));
        public string ClaimsAsString => string.Join(",", _claims.Select(x => x.Name));

        public void AddClaim(Claim claim) => _claims.Add(Guard.Against.Null(claim));
        public bool RemoveClaim(int claimId) => _claims.RemoveFirst(c => c.Id == claimId);

        public void AddRole(Role role) => _roles.Add(Guard.Against.Null(role));
        public bool RemoveRole(int roleId) => _roles.RemoveFirst(c => c.Id == roleId);

        public void AddRefreshTokenAndRevokeLast(string token, DateTime d, string? createdByIp)
        {
            _refreshTokens.LastOrDefault()?.Revoke(TokenRevokeTypeEnum.Replaced.ToString());
            _refreshTokens.Add(new RefreshToken(token, d, createdByIp));
        }

        public void AddRefreshTokenAndReplace(string token, DateTime d, string? createdByIp)
        {
            _refreshTokens.LastOrDefault()?.ReplaceToken(token);
            _refreshTokens.Add(new RefreshToken(token, d, createdByIp));
        }
    }
}
