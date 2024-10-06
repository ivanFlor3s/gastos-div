using Divtos.Application.Authentication.Commons;
using ErrorOr;
using MediatR;

namespace Divtos.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password): IRequest<ErrorOr<AuthenticationResult>>;
}
