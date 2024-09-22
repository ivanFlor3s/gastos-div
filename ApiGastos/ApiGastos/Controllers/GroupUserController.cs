using ApiGastos.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGastos.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/group-user")]
    [ApiController]
    public class GroupUserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public GroupUserController(IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        // GET: api/<GroupUserController>   
        [HttpDelete("group/{idGroup}/user/{idAppUser}")]
        public async Task<IActionResult> Delete(int idGroup, string idAppUser)
        {
            var groupUser = await this.context.GroupUsers
                .FirstOrDefaultAsync(gu => gu.GroupId == idGroup && gu.AppUserId == idAppUser);

            if (groupUser == null)
            {
                return NotFound();
            }

            this.context.GroupUsers.Remove(groupUser);
            await this.context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("group/{idGroup}")]
        public async Task<IActionResult> DeleteMySelfFromGroup (int idGroup)
        {
            var groupUser = await this.context.GroupUsers
                .FirstOrDefaultAsync(gu => gu.GroupId == idGroup && gu.AppUserId == User.Identity.GetId());

            if (groupUser == null)
            {
                return NotFound();
            }

            this.context.GroupUsers.Remove(groupUser);
            await this.context.SaveChangesAsync();

            return Ok();
        }


    }
}
