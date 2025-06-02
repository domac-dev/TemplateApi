using Ardalis.GuardClauses;
using Domain.Abstraction;
using Domain.Entities.Translation;
using Domain.Enumerations;
using System.Globalization;

namespace Domain.Entities.Core
{
    public class CultureType : AudiableEntity, IAggregateRoot
    {
        public int Id { get; private set; }
        public string Value { get; private set; } = null!;
        public CultureTypeEnum Type { get; private set; }
        public CultureInfo CultureInfo { get => CultureInfo.GetCultureInfo(Value); }

        protected CultureType() { }
        public CultureType(string value, CultureTypeEnum type)
        {
            Value = Guard.Against.Culture(value);
            Type = Guard.Against.EnumOutOfRange(type);
        }

        public TranslationCollection<CultureTypeTranslation> Translations { get; set; } = [];

        public string Content
        {
            get { return Translations[CultureInfo.CurrentCulture].Content; }
            set { Translations[CultureInfo.CurrentCulture].Content = value; }
        }
    }
}
