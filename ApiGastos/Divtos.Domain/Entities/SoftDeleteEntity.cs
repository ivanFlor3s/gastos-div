using System.ComponentModel.DataAnnotations.Schema;

namespace Divtos.Domain.Entities
{
    public class SoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedById { get; set; }
        
        [ForeignKey("DeletedById")]
        public User DeletedBy { get; set; }
    }
}
