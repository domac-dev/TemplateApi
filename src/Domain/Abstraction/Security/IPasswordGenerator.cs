namespace Domain.Abstraction.Security
{
    public interface IPasswordGenerator
    {
        string Generate();
        int Length { get; }
        int MinLowercases { get; }
        int MinUppercases { get; }
        int MinDigits { get; }
        int MinSpecials { get; }
    }
}
