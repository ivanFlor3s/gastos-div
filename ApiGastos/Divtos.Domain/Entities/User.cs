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
        public List<GroupUser> GroupUsers { get; set; }
        public List<SpentParticipant> SpentParticipants { get; set; } = new List<SpentParticipant>();

    }
}
