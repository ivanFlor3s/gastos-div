using System.ComponentModel.DataAnnotations.Schema;

namespace Divtos.Domain.Entities
{
    public abstract class Auditable
    {
        public DateTime CreatedAt { get; set; }
        public Guid CreatorId { get; set; }
        
        public DateTime? LastModified { get; set; }
        public Guid? LastModifiedById { get; set; }
        
        [ForeignKey("LastModifiedById")]
        public User LastModifiedBy { get; set; }

        [ForeignKey("CreatorId")]
        public User Creator { get; set; } = null!;
    }
}
