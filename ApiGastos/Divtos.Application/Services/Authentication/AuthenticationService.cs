using Divtos.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            // Check user exists

            return new AuthenticationResult(Guid.NewGuid(), "John", "Doe", email, "token");
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            //check user exists

            // create user

            // create token
            string userId = Guid.NewGuid().ToString();
            var token = _jwtTokenGenerator.GenerateToken(userId, email, firstName, lastName, DateTime.UtcNow.AddHours(1));

            return new AuthenticationResult(Guid.NewGuid(), firstName, lastName, email, token);
        }
    }
}
