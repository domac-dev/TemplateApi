using Ardalis.GuardClauses;
using Domain.Abstraction;
using Domain.Enumerations;

namespace Domain.Entities.Core
{
    public class Claim : AudiableEntity, IAggregateRoot
    {
        private readonly List<ApplicationUser> _users = [];
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public ClaimType Type { get; set; } = default!;
        protected Claim() { }
        public Claim(ClaimType claimType)
        {
            Type = Guard.Against.EnumOutOfRange(claimType);
            Name = Enum.GetName(claimType)!;
        }
        public IReadOnlyCollection<ApplicationUser> Users => _users;
    }
}
