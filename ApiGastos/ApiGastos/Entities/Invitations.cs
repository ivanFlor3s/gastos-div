using ApiGastos.Core.Share.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiGastos.Entities
{
    public class Invitation: Auditable
    {
        public int Id { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int GroupId { get; set; }    
        public InvitationStatus InvitationStatus { get; set;}
        public DateTime? AcceptedAt { get; set; }
        public Group? Group { get; set; }

    }
}
