namespace Application.Modules.Authentication.DTOs
{
    public record TokenDTO(string Value, DateTime ExpiresAt);
}
