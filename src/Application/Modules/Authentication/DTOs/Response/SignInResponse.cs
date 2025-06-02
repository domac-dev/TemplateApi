namespace Application.Modules.Authentication.DTOs.Response
{
    public record SignInResponseDTO(
        int Id,
        string FullName,
        string Telephone,
        string Email,
        string Culture,
        IList<string> Claims,
        IList<string> Roles,
        TokenDTO RefreshToken,
        TokenDTO AccessToken
    );
}
