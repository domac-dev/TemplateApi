namespace Domain.Abstraction
{
    public interface IUserCredentials
    {
        public int Id { get; }
        public string Email { get; }
        public string RolesAsString { get; }
        public string ClaimsAsString { get; }
        public string Culture { get; }
    }
}
