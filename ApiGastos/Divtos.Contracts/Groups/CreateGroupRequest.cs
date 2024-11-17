using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Contracts.Groups
{
    public class CreateGroupRequest
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
