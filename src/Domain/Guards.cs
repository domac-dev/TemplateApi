using Ardalis.GuardClauses;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Domain
{
    public static class TelephoneGuard
    {
        public static string Telephone(this IGuardClause guardClause, string input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            Guard.Against.NullOrWhiteSpace(input, parameterName);
            string pattern = @"^\+?[0-9\s\-]{7,15}$";

            if (!Regex.IsMatch(input, pattern))
                throw new ArgumentException("Invalid telephone format.", parameterName);

            return input;
        }

        public static string? TelephoneNullable(this IGuardClause guardClause, string? input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            string pattern = @"^\+?[0-9\s\-]{7,15}$";

            if (!Regex.IsMatch(input, pattern))
                throw new ArgumentException("Invalid telephone format.", parameterName);

            return input;
        }
    }



    public static class StringGuard
    {
        public static string MinMaxLength(this IGuardClause guardClause, string input, int minLength, int maxLength,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            if (input == null)
                throw new ArgumentNullException(parameterName, "Input string cannot be null.");

            if (input.Length < minLength || input.Length > maxLength)
                throw new ArgumentException($"String length must be between {minLength} and {maxLength} characters.", parameterName);

            return input;
        }

        public static string? MinMaxLengthNullable(this IGuardClause guardClause, string? input, int minLength, int maxLength,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            if (input != null && input.Length < minLength || input?.Length > maxLength)
                throw new ArgumentException($"String length must be between {minLength} and {maxLength} characters.", parameterName);

            return input;
        }

    }

    public static class IPGuard
    {
        public static string IP(this IGuardClause guardClause, string input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            Guard.Against.NullOrWhiteSpace(input, parameterName);

            if (!IPAddress.TryParse(input, out _))
                throw new ArgumentException("Invalid IP address format.", parameterName);

            return input;
        }

        public static string? IPNullable(this IGuardClause guardClause, string? input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (!IPAddress.TryParse(input, out _))
                throw new ArgumentException("Invalid IP address format.", parameterName);

            return input;
        }
    }

    public static class NullableGuard
    {
        public static T? NegativeOrZeroNullable<T>(this IGuardClause guardClause, T? input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null) where T : struct, IComparable<T>
        {
            if (input.HasValue && input.Value.CompareTo(default) <= 0)
                throw new ArgumentException("Value must not be negative or zero.", parameterName);

            return input;
        }

        public static T? OutOfRangeNullable<T>(this IGuardClause guardClause, T? value, string? parameterName, T minValue, T maxValue) where T : struct, IComparable<T>
        {
            if (value.HasValue && (value.Value.CompareTo(minValue) < 0 || value.Value.CompareTo(maxValue) > 0))
                throw new ArgumentOutOfRangeException(parameterName, $"The value must be between {minValue} and {maxValue}.");

            return value;
        }
    }

    public static class GuidGuard
    {
        public static string Guid(this IGuardClause guardClause, string input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            Guard.Against.NullOrWhiteSpace(input, parameterName);

            if (!System.Guid.TryParse(input, out Guid guid))
                throw new ArgumentException("Invalid GUID format.", parameterName);

            return input;
        }

        public static string? GuildNullable(this IGuardClause guardClause, string? input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (!System.Guid.TryParse(input, out Guid guid))
                throw new ArgumentException("Invalid GUID format.", parameterName);

            return input;
        }
    }

    public static class EnumNullableGuard
    {
        public static TEnum? EnumOutOfRangeNullable<TEnum>(this IGuardClause guardClause,
            TEnum? input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
            where TEnum : struct, Enum
        {
            if (input is not null && !Enum.IsDefined(typeof(TEnum), input))
                throw new ArgumentOutOfRangeException(parameterName, $"Enum value '{input}' is not valid.");

            return input;
        }
    }

    public static class EmailGuard
    {
        public static string InvalidEmailFormat(this IGuardClause guardClause, string input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            if (string.IsNullOrEmpty(input) || !IsValidEmailFormat(input))
                throw new ArgumentException("Invalid email format", parameterName);

            return input;
        }

        public static string? InvalidEmailFormatNullable(this IGuardClause guardClause, string? input,
            [CallerArgumentExpression(nameof(input))] string? parameterName = null)
        {
            if (!IsValidEmailFormat(input))
                throw new ArgumentException("Invalid email format", parameterName);

            return input;
        }

        private static bool IsValidEmailFormat(string? email)
        {
            if (email == null) return true;
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
