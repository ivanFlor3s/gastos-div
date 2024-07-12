using ApiGastos.Entities;
using ApiGastos.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Dtos.Spent
{
    public class AddSpentDto
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MinLength(1, ErrorMessage = "Al menos debe haber 1 usuario regitrado en el gasto")]
        public List<NameValue<string>> Users { get; set; }
        [Required]
        public SpentMode How { get; set; }
        
        [Required]
        public string By { get; set; }  

        [Required]
        public int GroupId { get; set; }
        
    }
}
