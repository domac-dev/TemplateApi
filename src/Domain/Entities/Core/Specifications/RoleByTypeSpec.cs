using Ardalis.Specification;
using Domain.Entities.Core;
using Domain.Enumerations;

namespace Domain.Entities.Core.Specifications
{
    public class RoleByTypeSpec : Specification<Role>
    {
        public RoleByTypeSpec(RoleType roleType)
        {
            Query.Where(role => role.Type == roleType);
        }
    }
}
