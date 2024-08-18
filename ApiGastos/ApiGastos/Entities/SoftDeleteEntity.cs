namespace ApiGastos.Entities
{
    public class SoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedById { get; set; }
    }
}
