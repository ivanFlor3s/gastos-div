using System.Security.Claims;
using Divtos.Application.Common;
using Divtos.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;

namespace Divtos.Infraestructure.Security.CurrentUserProvider;

public class CurrentUserProvider: ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public CurrentUser GetCurrentUser()
    {
        var id = GetSingleClaimValue(ClaimTypes.NameIdentifier);
        var email = GetSingleClaimValue(ClaimTypes.Email);
        var firstName = GetSingleClaimValue(ClaimTypes.Name);
        var lastName = GetSingleClaimValue(ClaimTypes.Surname);
        
        return new CurrentUser(Guid.Parse(id), firstName, lastName, email);
    }
    
    private List<string> GetClaimValues(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();

    private string GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value;

}