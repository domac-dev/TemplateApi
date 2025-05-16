using Ardalis.Specification;

namespace Domain.Entities.Core.Specifications
{
    public class UserByRefreshTokenSpec : Specification<ApplicationUser>
    {
        public UserByRefreshTokenSpec(string token)
        {
            Query.Include(c => c.RefreshTokens).Where(c => c.RefreshTokens.Any(tok => tok.Token == token
                && tok.ExpiresAt > DateTime.UtcNow
                && tok.ReasonRevoked == null));
        }
    }
}
