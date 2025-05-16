using Ardalis.Specification;

namespace Domain.Entities.Core.Specifications
{
    public class UserByConfirmationTokenSpec : Specification<ApplicationUser>
    {
        public UserByConfirmationTokenSpec(string confirmationToken)
        {
            Query.Where(c=> c.EmailConfirmationToken == confirmationToken && !c.EmailConfirmed &&
                (!c.ValidUntil.HasValue || c.ValidUntil.Value < DateTime.UtcNow));
        }
    }
}
