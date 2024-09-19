using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Entities
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        public List<GroupUser> GroupUsers { get; set; }
        public List<SpentParticipant> SpentParticipants { get; set; } = new List<SpentParticipant>();
        public bool GoogleSignedIn { get; set; }
        public string ImageUrl { get; set; }    
        public bool IsTemporal { get; set;}

    }
}
