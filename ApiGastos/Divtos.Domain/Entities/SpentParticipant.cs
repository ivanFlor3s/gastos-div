using System.ComponentModel.DataAnnotations.Schema;

namespace Divtos.Domain.Entities
{
    public class SpentParticipant
    {
        public Guid UserId { get; set; }
        public int SpentId { get; set; }
        
        [ForeignKey("SpentId")]
        public Spent Spent { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
