using Domain.Enumerations;

namespace Domain.Abstraction
{
    public interface IUserCredentials
    {
        public long Id { get; }
        public string Email { get; }
        public string RolesAsString { get; }
        public string ClaimsAsString { get; }
        public CultureType Culture { get; }
    }
}
