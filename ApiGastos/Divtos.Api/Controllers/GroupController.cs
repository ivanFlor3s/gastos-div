using Divtos.Application.Groups;
using Divtos.Application.Groups.Commands.Create;
using Divtos.Contracts.Groups;
using DivtosApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Divtos.Api.Controllers
{
    [Route("api/v2/groups")]
    [Authorize]
    public class GroupController : ApiBaseController
    {
        private readonly ISender _mediator;

        public GroupController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
        {
            var command = new CreateGroupCommand(request.Name,
                                                 request.Description,
                                                 request.ImageUrl,
                                                 request.Emails);

            var result = await _mediator.Send(command);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

    }
}
