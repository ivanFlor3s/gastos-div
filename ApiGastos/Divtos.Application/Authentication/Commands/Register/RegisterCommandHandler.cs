using Divtos.Application.Authentication.Commons;
using Divtos.Application.Common.Interfaces.Authentication;
using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Domain.Commons.Errors;
using Divtos.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Divtos.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            //check user exists
            if (_userRepository.GetByEmailAsync(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // create user
            var user = new User
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Password = command.Password,
            };
            await _userRepository.AddAsync(user);

            // create token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
