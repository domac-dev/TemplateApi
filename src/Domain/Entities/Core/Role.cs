using Ardalis.GuardClauses;
using Domain.Abstraction;
using Domain.Enumerations;

namespace Domain.Entities.Core
{
    public class Role : AudiableEntity, IAggregateRoot
    {
        private readonly List<ApplicationUser> _users = [];
        protected Role() { }
        public Role(RoleType roleType)
        {
            Type = Guard.Against.EnumOutOfRange(roleType);
            Name = Enum.GetName(roleType)!;
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public RoleType Type { get; set; } = default!;
        public IReadOnlyCollection<ApplicationUser> Users => _users;
    }
}
