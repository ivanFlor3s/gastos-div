namespace Divtos.Application.Groups.Commons;

public record GroupMemberResult(int Id, string FirstName, string LastName, string Email, string ImageUrl, bool IsAdmin, bool IsTemporal)
{
    public string FullName => $"{FirstName} {LastName}";
};