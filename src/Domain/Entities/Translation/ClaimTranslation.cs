namespace Domain.Entities.Translation
{
    public class ClaimTranslation : Translation<ClaimTranslation>
    {
        public int ClaimId { get; set; }
        public string Content { get; set; } = null!;
    }
}
