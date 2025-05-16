namespace Application.Modules.Authentication.DTOs.Response
{
    public class RefreshTokenResponseDTO
    {
        public TokenDTO RefreshToken { get; set; } = null!;
        public TokenDTO AccessToken { get; set; } = null!;
    }
}
