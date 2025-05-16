namespace Domain.Entities.Core
{
    public class UserClaim : AudiableEntity
    {
        public long UserId { get; set; }
        public int ClaimId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public Claim Claim { get; set; } = null!;
        public UserClaim() { }
    }
}
