using Ardalis.GuardClauses;
using Domain.Abstraction;
using Domain.Entities.Core;
using Domain.Enumerations;

namespace Domain.Entities.Security
{
    public class Role : AudiableEntity, IAggregateRoot
    {
        protected Role() { }
        public Role(RoleTypeEnum roleType)
        {
            Type = Guard.Against.EnumOutOfRange(roleType);
            Name = Enum.GetName(roleType)!;
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public RoleTypeEnum Type { get; set; } = default!;


        private readonly List<User> _users = [];
        public IReadOnlyCollection<User> Users => _users;
    }
}
