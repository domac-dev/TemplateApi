namespace Application.Modules.Authentication.DTOs.Response
{
    public class SignInResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Culture { get; set; } = null!;
        public IList<string> Claims { get; set; } = null!;
        public IList<string> Roles { get; set; } = null!;
        public TokenDTO RefreshToken { get; set; } = null!;
        public TokenDTO AccessToken { get; set; } = null!;
    }
}
