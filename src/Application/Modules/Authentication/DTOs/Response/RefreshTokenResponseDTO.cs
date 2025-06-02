namespace Application.Modules.Authentication.DTOs.Response
{
    public record RefreshTokenResponseDTO(TokenDTO RefreshToken, TokenDTO AccessToken);
}
