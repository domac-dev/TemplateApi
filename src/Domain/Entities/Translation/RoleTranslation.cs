namespace Domain.Entities.Translation
{
    public class RoleTranslation : Translation<RoleTranslation>
    {
        public int RoleId { get; set; }
        public string Content { get; set; } = null!;
    }
}
