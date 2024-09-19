using ApiGastos.Entities;

namespace ApiGastos.Dtos
{
    public class UserCreationDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsTemporal { get; set; }
    }
}
