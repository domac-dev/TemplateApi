using Domain;
using System.Linq.Expressions;

namespace Application
{
    internal static class ExpressionHelper
    {
        internal static Expression<Func<T, bool>> Valid<T>() where T : AudiableEntity
        {
            return entity => !entity.ValidUntil.HasValue || entity.ValidUntil < DateTime.UtcNow;
        }
    }
}
