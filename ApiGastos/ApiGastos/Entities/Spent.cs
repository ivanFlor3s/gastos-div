using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Entities
{
    public class Spent
    {
        [Key]
        public int SpendId {get; set;}
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a {1}")]
        public decimal Amount { get; set;}
        [Required]
        public DateTime CreatedAt { get; set;}
        [MaxLength(100)]
        [Required]
        public string Description { get; set;}
        [Required]
        public Boolean IsDeleted { get; set;}
        [Required] 
        public Boolean IsLiquidacion { get; set;}
        public GroupUser GroupUser { get; set;}


    }
}
