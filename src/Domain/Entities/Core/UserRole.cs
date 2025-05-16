namespace Domain.Entities.Core
{
    public class UserRole : AudiableEntity
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public Role Role { get; set; } = null!;
        public UserRole() { }
    }
}
