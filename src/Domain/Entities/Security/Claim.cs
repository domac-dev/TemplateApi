using Ardalis.GuardClauses;
using Domain.Abstraction;
using Domain.Entities.Core;
using Domain.Enumerations;

namespace Domain.Entities.Security
{
    public class Claim : AudiableEntity, IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ClaimTypeEnum Type { get; set; } = default!;
        protected Claim() { }
        public Claim(ClaimTypeEnum claimType)
        {
            Type = Guard.Against.EnumOutOfRange(claimType);
            Name = Enum.GetName(claimType)!;
        }

        private readonly List<User> _users = [];
        public IReadOnlyCollection<User> Users => _users;
    }
}
