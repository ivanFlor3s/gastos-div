using Divtos.Application.Authentication.Commons;
using ErrorOr;
using MediatR;

namespace Divtos.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
