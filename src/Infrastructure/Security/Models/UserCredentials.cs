using Domain.Abstraction;
using Domain.Enumerations;
using Newtonsoft.Json;

namespace Infrastructure.Security.Models
{
    public class UserCredentials : IUserCredentials
    {
        public UserCredentials() { }
        public UserCredentials(long id, string email, string roles, string claims, CultureType cultureType)
        {
            Id = id;
            Email = email;
            RolesAsString = roles;
            ClaimsAsString = claims;
            Culture = cultureType;
        }

        public long Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string RolesAsString { get; private set; } = string.Empty;
        public string ClaimsAsString { get; private set; } = string.Empty;
        public CultureType Culture { get; private set; } = CultureType.Croatian;

        public static UserCredentials FromUser(IUserCredentials user)
        {
            return new UserCredentials(
                user.Id,
                user.Email,
                user.RolesAsString,
                user.ClaimsAsString,
                user.Culture
            );
        }

        public static UserCredentials FromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("JSON string cannot be null or empty.", nameof(json));

            var userCredentials = JsonConvert.DeserializeObject<UserCredentials>(json);
            return userCredentials ?? throw new Exception("Deserialization failed. Please check the JSON format.");
        }

    }
}
