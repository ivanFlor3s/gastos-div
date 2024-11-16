using Divtos.Application.Authentication.Commons;
using Divtos.Application.Common.Interfaces.Authentication;
using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Domain.Commons.Errors;
using Divtos.Domain.Entities;
using ErrorOr;
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
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Login(string email, string password)
        {
            // Check user exists
            if(await _userRepository.GetByEmailAsync(email) is not User user)
            {
                return Errors.User.DuplicateEmail;
            }

            // Check password
           

            // Create token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

       
    }
}
