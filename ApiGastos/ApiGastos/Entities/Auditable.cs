namespace ApiGastos.Entities
{
    public abstract class Auditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        public string CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
