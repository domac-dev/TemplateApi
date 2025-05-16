using Ardalis.GuardClauses;
using Domain.Abstraction.Security;

namespace Infrastructure.Security.Models
{
    public record Token(string Value, DateTime ExpiresAt) : IToken
    {
        public DateTime ExpiresAt { get; init; } = Guard.Against.Null(ExpiresAt);
        public string Value { get; init; } = Value;
    }
}
