using Divtos.Application.Authentication.Commons;
using Divtos.Application.Common.Interfaces.Authentication;
using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Domain.Commons.Errors;
using Divtos.Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Divtos.Domain.Commons.Errors.Errors;

namespace Divtos.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUnitOfWork _unitOfWork;


        public LoginQueryHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Check user exists
            if (await _unitOfWork.Users.GetByEmailAsync(query.Email) is not Domain.Entities.User user)
            {
                return Errors.Authentication.UserNotFound;
            }

            // Check password
            if (user.Password != query.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // Create token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
