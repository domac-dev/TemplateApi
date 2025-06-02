namespace Domain.Abstraction
{
    public interface IRepository<T> : IReadRepository<T> where T : IAggregateRoot
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        void Add(T entity);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
