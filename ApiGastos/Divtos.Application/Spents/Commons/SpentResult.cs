namespace Divtos.Application.Spents.Commons;

public record SpentResult(
    int Id,
    int GroupId,
    decimal Amount,
    string Description,
    string AuthorId,
    DateTime PayedAt,
    DateTime CreatedAt
    )
{
    
}