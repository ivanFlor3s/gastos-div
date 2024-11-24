namespace Divtos.Application.Common;

public record CurrentUser(
    Guid Id, 
    string FirstName, 
    string LastName,
    string Email);