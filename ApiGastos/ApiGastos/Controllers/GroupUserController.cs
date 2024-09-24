using ApiGastos.Dtos;
using ApiGastos.Entities;
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

        [HttpPut("group/{idGroup}")]
        public async Task<IActionResult> Put(int idGroup, [FromBody] GroupMembersUpdateDto input)
        {
            var groupUsersDb = await context.GroupUsers
                .Where(gu => gu.GroupId == idGroup)
                .Include(gu => gu.AppUser)
                .ToListAsync();

            if (groupUsersDb.Count == 0)
            {
                return NotFound();
            }



            foreach (var userId in input.AddedUsers)
            {
                if (groupUsersDb.Any(gu => gu.AppUserId == userId))
                {
                    continue;
                }

                var groupUser = new GroupUser
                {
                    AppUserId = userId,
                    GroupId = idGroup
                };

                context.GroupUsers.Add(groupUser);
            }

            foreach (var userId in input.DeletedUsers)
            {
                var groupUser = groupUsersDb.FirstOrDefault(gu => gu.AppUserId == userId);

                if (groupUser == null)
                {
                    continue;
                }

                context.GroupUsers.Remove(groupUser);
            }

            await context.SaveChangesAsync();
            return Ok();
        }   


    }
}
