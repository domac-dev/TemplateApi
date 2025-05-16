using Ardalis.GuardClauses;
using Domain.Enumerations;

namespace Domain.Entities.Core
{
    public class RefreshToken : AudiableEntity
    {
        public long Id { get; set; }
        public string Token { get; private set; } = null!;
        public long UserId { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; } = null!;
        public DateTime ExpiresAt { get; private set; }
        public string? ReplacedByToken { get; private set; }
        public string CreatedByIp { get; private set; } = null!;
        public string? RevokedBy { get; private set; }
        public string? ReasonRevoked { get; private set; }
        public DateTime? RevokedAt { get; private set; }

        protected RefreshToken() { }
        public RefreshToken(string token, DateTime expiresAt, string? createdByIp = null)
        {
            Token = Guard.Against.MinMaxLength(token, 12, ModelConstants.TokenModel.VALUE);
            CreatedByIp = Guard.Against.IPNullable(createdByIp) ?? "UNKNOWN";
            ExpiresAt = Guard.Against.NullOrOutOfSQLDateRange(expiresAt);
        }

        public void ReplaceToken(string refreshToken)
        {
            Revoke(TokenRevokeType.Replaced.ToString());
            ReplacedByToken = refreshToken;
        }

        public void Revoke(string message)
        {
            ReasonRevoked = message;
            RevokedAt = DateTime.UtcNow;
        }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsRevoked => RevokedAt.HasValue;
        public bool IsActivated => !IsRevoked && !IsExpired;
    }
}
