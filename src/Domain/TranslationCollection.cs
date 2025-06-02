using System.Collections.ObjectModel;
using System.Globalization;

namespace Domain
{
    public class TranslationCollection<T> : Collection<T> where T : Translation<T>, new()
    {
        public T this[CultureInfo culture]
        {
            get
            {
                T? translation = this.FirstOrDefault(x => x.CultureName == culture.Name);
                if (translation is null)
                {
                    translation = new T
                    {
                        CultureName = culture.Name
                    };
                    Add(translation);
                }

                return translation;
            }
            set
            {
                T? translation = this.FirstOrDefault(x => x.CultureName == culture.Name);
                if (translation != null)
                {
                    Remove(translation);
                }
                value.CultureName = culture.Name;
                Add(value);
            }
        }

        public T this[string culture]
        {
            get
            {
                T? translation = this.FirstOrDefault(x => x.CultureName == culture);
                if (translation == null)
                {
                    translation = new T
                    {
                        CultureName = culture
                    };
                    Add(translation);
                }

                return translation;
            }
            set
            {
                T? translation = this.FirstOrDefault(x => x.CultureName == culture);
                if (translation != null)
                {
                    Remove(translation);
                }

                value.CultureName = culture;
                Add(value);
            }
        }

        public bool HasCulture(string culture) => this.Any(x => x.CultureName == culture);
        public bool HasCulture(CultureInfo culture) => this.Any(x => x.CultureName == culture.Name);
    }
}
