using Ardalis.Specification;

namespace Domain.Entities.Core.Specifications
{
    public class UserByEmailSpec : Specification<ApplicationUser>
    {
        public UserByEmailSpec(string email)
        {
            Query.Where(c => c.Email == email && c.EmailConfirmed && 
                (!c.ValidUntil.HasValue || c.ValidUntil.Value < DateTime.UtcNow));
        }
    }
}
