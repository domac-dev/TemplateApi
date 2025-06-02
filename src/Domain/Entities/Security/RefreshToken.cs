using Ardalis.GuardClauses;
using Domain.Entities.Core;
using Domain.Enumerations;

namespace Domain.Entities.Security
{
    public class RefreshToken : AudiableEntity
    {
        public int Id { get; set; }
        public string Token { get; private set; } = null!;
        public int UserId { get; private set; }
        public User User { get; private set; } = null!;
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
            CreatedByIp = !string.IsNullOrEmpty(createdByIp) && createdByIp != AppConstants.UNKWOWN
                ? Guard.Against.IP(createdByIp) : AppConstants.UNKWOWN;
            ExpiresAt = Guard.Against.NullOrOutOfSQLDateRange(expiresAt);
        }

        public void ReplaceToken(string refreshToken)
        {
            Revoke(TokenRevokeTypeEnum.Replaced.ToString());
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
