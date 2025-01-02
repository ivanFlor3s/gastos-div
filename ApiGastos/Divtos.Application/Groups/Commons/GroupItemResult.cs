using Divtos.Application.Spents.Commons;

namespace Divtos.Application.Groups.Commons;

public class GroupItemResult {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ImageUrl { get; set; }
    public decimal TotalSpent { get; set; }
    public bool IsAdmin { get; set; }
    public List<GroupMember> Users { get; set; }
    // List<SpentResult> Spents
}