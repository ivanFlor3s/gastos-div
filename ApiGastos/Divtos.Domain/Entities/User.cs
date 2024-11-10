using Divtos.Domain.Commons;

namespace Divtos.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool GoogleSignIn { get; set; } = false;

    }
}
