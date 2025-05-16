namespace Domain
{
    public class AppSettings
    {
        public AuthenticationConfig AuthenticationConfig { get; set; } = null!;
        public string Domain { get; set; } = null!;
    }

    public record AuthenticationConfig
    {
        // The authority URL for the identity provider (e.g., OAuth, OpenID Connect server).
        // This is typically the URL where tokens are issued and validated.
        public string Authority { get; set; } = null!;

        // The secret key used for authentication with the token issuer (e.g., client secret).
        // This is required to authenticate against the identity provider.
        public string Secret { get; set; } = null!;

        // Indicates whether HTTPS metadata retrieval is required when validating tokens.
        // Should typically be set to 'true' in production environments to ensure secure communications.
        public bool RequireHttpsMetadata { get; set; }

        // Determines whether the audience of the token should be validated.
        // This ensures that the token was issued for the intended recipient (your application).
        public bool ValidateAudience { get; set; }

        // Determines whether the issuer of the token should be validated.
        // This ensures that the token was issued by a trusted identity provider (Authority).
        public bool ValidateIssuer { get; set; }

        // The expected issuer of the token, which should match the token's 'iss' claim.
        // Typically the URL of the identity provider.
        public string Issuer { get; set; } = null!;

        // The expected audience of the token, which should match the token's 'aud' claim.
        // This is typically the name of your API or application.
        public string Audience { get; set; } = null!;

        // The time (in minutes) for which an access token remains valid.
        // After this period, the token will expire, and a new one will need to be issued.
        public int AccessTokenExpiration { get; set; }

        // The time (in minutes) for which a refresh token remains valid.
        // A refresh token allows the client to obtain a new access token without re-authentication.
        public int RefreshTokenExpiration { get; set; }
    }

}
