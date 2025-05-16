namespace Domain.Enumerations
{
    public enum CultureType
    {
        Croatian = 1,
        English
    }

    public static class CultureExtensions
    {
        public static CultureType FromString(this string culture)
        {
            return culture switch
            {
                "hr-HR" => CultureType.Croatian,
                "en-US" => CultureType.English,
                _ => CultureType.English,
            };
        }

        public static string AsString(this CultureType? languageType)
        {
            return languageType switch
            {
                CultureType.Croatian => "hr-HR",
                CultureType.English => "en-US",
                _ => "en-US",
            };
        }
    }
}
