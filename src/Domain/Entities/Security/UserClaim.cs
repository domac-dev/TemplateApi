using Domain.Entities.Core;

namespace Domain.Entities.Security
{
    public class UserClaim : AudiableEntity
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
        public User User { get; set; } = null!;
        public Claim Claim { get; set; } = null!;
        public UserClaim() { }
    }
}
