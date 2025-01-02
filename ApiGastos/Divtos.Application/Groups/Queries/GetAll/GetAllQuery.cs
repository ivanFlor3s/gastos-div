using Divtos.Application.Groups.Commons;
using ErrorOr;
using MediatR;

namespace Divtos.Application.Groups.Queries.GetAll;

public record GetAllQuery : IRequest<ErrorOr<ICollection<GroupItemResult>>>
{
    
}