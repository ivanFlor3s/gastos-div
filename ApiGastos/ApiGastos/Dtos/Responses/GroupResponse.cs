using ApiGastos.Entities;
using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Dtos.Responses
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<AppUserResponse> Users { get; set; }
        public List<SpentResponse> Spents { get; set; }
    }
}
