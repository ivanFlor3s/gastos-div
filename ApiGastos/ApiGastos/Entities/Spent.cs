using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Entities
{
    public class Spent : Auditable
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Required]
        public string AuthorId { get; set; }
        
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El campo {0} debe ser mayor a {1}")]
        public decimal Amount { get; set; }
        
        [Required]
        public DateTime PayedAt { get; set; }

        [MaxLength(50)]
        [Required]
        public string Description { get; set; }
        public SpentMode SpentMode { get; set; }
        public Group Group { get; set; } = null!;
        public AppUser Author { get; set; }

        [MinLength(1, ErrorMessage = "Al menos debe haber 1 usuario regitrado en el gasto")]
        public List<SpentParticipant> Participants { get; set; } = new List<SpentParticipant>();

    }
}
