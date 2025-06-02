namespace Domain
{
    public abstract class Translation<T> : AudiableEntity where T : Translation<T>, new()
    {
        public Guid Id { get; set; }
        public string CultureName { get; set; } = null!;
        protected Translation() => Id = Guid.NewGuid();
    }
}
