namespace Divtos.Application.Common.Interfaces.Authentication;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}