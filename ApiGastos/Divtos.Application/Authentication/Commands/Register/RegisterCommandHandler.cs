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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork, IPasswordService passwordService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            //check user exists
            if (await _unitOfWork.Users.GetByEmailAsync(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // create user
            var user = new User
            {
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PasswordHash = _passwordService.HashPassword(command.Password)
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            // create token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
