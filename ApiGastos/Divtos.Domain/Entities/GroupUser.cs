using System.ComponentModel.DataAnnotations.Schema;

namespace Divtos.Domain.Entities
{
    public class GroupUser 

    {
        public int GroupId { get; set; }
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }

        [ForeignKey("GroupId")]
        public Group Group { get; set; } = null!;

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
    