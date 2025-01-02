namespace Divtos.Application.Groups.Commons;

public class GroupMember
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsTemporal { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
    
