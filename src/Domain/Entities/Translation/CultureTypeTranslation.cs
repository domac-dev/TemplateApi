namespace Domain.Entities.Translation
{
    public class CultureTypeTranslation : Translation<CultureTypeTranslation>
    {
        public int CultureTypeId { get; set; }
        public string Content { get; set; } = null!;
    }
}
