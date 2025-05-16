namespace Domain
{
    public abstract class AudiableEntity
    {
        protected AudiableEntity() { }
        public string CreatedBy { get; set; } = "system";
        public string? ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
        public DateTime? ValidUntil { get; set; }
        public bool IsValid => !ValidUntil.HasValue || ValidUntil.HasValue && ValidUntil.Value < DateTime.UtcNow;
    }
}
