using Domain.Entities.Core;

namespace Domain.Entities.Security
{
    public class UserRole : AudiableEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
        public UserRole() { }
    }
}
