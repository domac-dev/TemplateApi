using Domain;
using Domain.Abstraction;
using Newtonsoft.Json;

namespace Infrastructure.Security.Models
{
    public class UserCredentials : IUserCredentials
    {
        public UserCredentials() { }
        public UserCredentials(int id, string email, string roles, string claims, string cultureType)
        {
            Id = id;
            Email = email;
            RolesAsString = roles;
            ClaimsAsString = claims;
            Culture = cultureType;
        }

        public int Id { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string RolesAsString { get; private set; } = string.Empty;
        public string ClaimsAsString { get; private set; } = string.Empty;
        public string Culture { get; private set; } = CultureConstants.CROATIAN;

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
