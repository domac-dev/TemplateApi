using Ardalis.GuardClauses;
using Domain.Abstraction;
using Domain.Entities.Core;
using Domain.Entities.Translation;
using Domain.Enumerations;
using System.Globalization;

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

        public TranslationCollection<RoleTranslation> Translations { get; set; } = [];
        public string Content
        {
            get { return Translations[CultureInfo.CurrentCulture].Content; }
            set { Translations[CultureInfo.CurrentCulture].Content = value; }
        }

        private readonly List<User> _users = [];
        public IReadOnlyCollection<User> Users => _users;
    }
}
