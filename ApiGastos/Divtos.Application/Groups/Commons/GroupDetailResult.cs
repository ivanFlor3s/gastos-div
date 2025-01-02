namespace Divtos.Application.Groups.Commons;

public record GroupDetailResult(
    int Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    string ImageUrl,
    List<string> Emails,
    bool IsAdmin,
    List<GroupMember> Users);