using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Dtos
{
    public class GroupCreationDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public List<string> Emails { get; set; } = new List<string>();
    }
}
