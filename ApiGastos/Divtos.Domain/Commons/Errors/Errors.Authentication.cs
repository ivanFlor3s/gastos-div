using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Domain.Commons.Errors
{
    public partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation("Auth.InvalidCredentials", "Invalid credentials.");
            public static Error UserNotFound => Error.NotFound("Auth.UserNotFound", "User not found.");
        }
    }
}
