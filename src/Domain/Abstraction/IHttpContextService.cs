namespace Domain.Abstraction
{
    public interface IHttpContextService
    {
        string IPAddress();
        string Culture();
        string BearerToken();
    }
}
