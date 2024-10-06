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

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // Check user exists
            if(_userRepository.GetByEmail(email) is not User user)
            {
                return Errors.User.DuplicateEmail;
            }

            // Check password
            if(user.Password != password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // Create token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            //check user exists
            if (_userRepository.GetByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // create user
            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password
            };
            _userRepository.Add(user);

            // create token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
