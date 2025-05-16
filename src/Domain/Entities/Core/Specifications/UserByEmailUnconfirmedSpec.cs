using Ardalis.Specification;

namespace Domain.Entities.Core.Specifications
{
    public class UserByEmailUnconfirmedSpec : Specification<ApplicationUser>
    {
        public UserByEmailUnconfirmedSpec(string email)
        {
            Query.Include(x => x.Roles).Include(x => x.Claims).Include(x => x.RefreshTokens)
                .Where(c => c.Email == email && (!c.ValidUntil.HasValue || c.ValidUntil.Value < DateTime.UtcNow));
        }
    }
}
