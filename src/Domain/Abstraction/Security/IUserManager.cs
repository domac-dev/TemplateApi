namespace Domain.Abstraction.Security
{
    public interface IUserManager
    {
        IUserCredentials UserCredentials { get; }
        bool IsInRole(string role);
        bool HasClaim(string claim);
    }
}
