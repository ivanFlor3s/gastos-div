using ErrorOr;
using MediatR;

namespace Divtos.Application.Groups.Commands.Create
{
    public record CreateGroupCommand(string Name,
                                     string Description,
                                     string ImageUrl,
                                     List<string> Emails) : IRequest<ErrorOr<int>>;

}
