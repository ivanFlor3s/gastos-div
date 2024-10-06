using Divtos.Domain.Entities;

namespace Divtos.Application.Authentication.Commons
{
    public record AuthenticationResult(
       User User,
       string Token);
}
