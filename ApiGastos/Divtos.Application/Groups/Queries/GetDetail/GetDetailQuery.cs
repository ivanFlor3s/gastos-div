using Divtos.Application.Groups.Commons;
using MediatR;
using ErrorOr;

namespace Divtos.Application.Groups.Queries.GetDetail;

public record GetDetailQuery (int IdGroup): IRequest<ErrorOr<GroupDetailResult>>;