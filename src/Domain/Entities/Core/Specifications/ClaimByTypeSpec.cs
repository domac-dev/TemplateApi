using Ardalis.Specification;
using Domain.Enumerations;


namespace Domain.Entities.Core.Specifications
{
    public class ClaimByTypeSpec : Specification<Claim>
    {
        public ClaimByTypeSpec(ClaimType claimType)
        {
            Query.Where(claim => claim.Type == claimType);
        }
    }
}
