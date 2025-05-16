namespace Application.Modules.Authentication.DTOs
{
    public record class TokenDTO(string Token, DateTime ExpiresAt)
    {
        public string Token { get; } = Token;
        public DateTime ExpiresAt { get; } = ExpiresAt;
    }
}
