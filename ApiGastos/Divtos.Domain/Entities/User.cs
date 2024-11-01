using Divtos.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set;} = null!;
        public string Email { get; set;} = null!;
        public string Password { get; set; } = null!;
        public bool GoogleSignIn { get; set; } = false;

    }
}
