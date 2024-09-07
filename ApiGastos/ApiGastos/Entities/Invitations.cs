using ApiGastos.Core.Share.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Entities
{
    public class Invitation
    {
        public int Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int GroupId { get; set; }    
        public DateTime CreatedAt { get; set; }
        public InvitationStatus InvitationStatus { get; set;}
        public DateTime? AcceptedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public Group? Group { get; set; }

    }
}
