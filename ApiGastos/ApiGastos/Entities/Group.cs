using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Entities
{
    public class Group : Auditable
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
     
        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        
        public string ImageUrl { get; set; } = string.Empty;
        
        public List<GroupUser> GroupUsers { get; set; }

    }
}
