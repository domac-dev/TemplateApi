namespace Domain.Extensions
{
    public static class ListExtensions
    {
        public static bool RemoveFirst<T>(this IList<T> list, Func<T, bool> predicate)
        {
            ArgumentNullException.ThrowIfNull(list);
            ArgumentNullException.ThrowIfNull(predicate);

            T? item = list.FirstOrDefault(predicate);
            if (item is not null)
                return list.Remove(item);

            return false;
        }
    }
}
