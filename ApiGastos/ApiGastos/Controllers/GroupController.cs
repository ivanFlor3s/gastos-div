using ApiGastos.Core.Share.Enums;
using ApiGastos.Dtos;
using ApiGastos.Dtos.Responses;
using ApiGastos.Entities;
using ApiGastos.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGastos.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public GroupController(IMapper mapper, ApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        // GET: api/<GroupController>
        [HttpGet]
        public async Task<List<GroupResponse>> Get()
        {

            var groups = await this.context.Groups
                .Include(groupDb => groupDb.GroupUsers)
                .ThenInclude(groupUserDb => groupUserDb.AppUser)
                .Include(group => group.Spents)
                .ToListAsync();



            var mappedGroups = mapper.Map<List<GroupResponse>>(groups);

            #region Add isAdmin and TotalSpent to GroupResponse 
            foreach (var group in mappedGroups)
            {
                var user = group.Users.FirstOrDefault(u => u.Id == User.Identity.GetId());
                if (user != null)
                {
                    group.IsAdmin = user.IsAdmin;
                }
                group.TotalSpent = group.Spents.Sum(spent => spent.Amount);
            }
            #endregion






            return mappedGroups;
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var group = await this.context.Groups
                .Include(groupDb => groupDb.GroupUsers)
                    .ThenInclude(groupUserDb => groupUserDb.AppUser)
                .Include(group => group.Spents)
                    .ThenInclude(spents => spents.Author)
                .Include(group => group.Spents)
                    .ThenInclude(spents => spents.Participants)
                        .ThenInclude(participant => participant.User)
                .OrderByDescending(group => group.CreatedAt)
                .FirstOrDefaultAsync(groupDb => groupDb.Id == id);

            var result = mapper.Map<GroupResponse>(group);

            return Ok(result);
        }

        [HttpGet("{id}/basic")]
        public async Task<IActionResult> GetBasic(int id, [FromQuery] bool excludeCurrentUser)
        {
            var groupQuery = this.context.Groups
                .Include(groupDb => groupDb.GroupUsers)
                    .ThenInclude(groupUserDb => groupUserDb.AppUser)
                .Where(groupDb => groupDb.Id == id)
                .AsQueryable();

            if (excludeCurrentUser)
            {
                groupQuery = groupQuery
                    .Select(g => new Group
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Description = g.Description,
                        CreatedAt = g.CreatedAt,
                        GroupUsers = g.GroupUsers.Where(gu => gu.AppUserId != User.Identity.GetId()).ToList()
                    });
            }


            var group = await groupQuery.FirstOrDefaultAsync();

            if (group == null)
            {
                return NotFound();
            }

            var result = mapper.Map<GroupBasicResponse>(group);

            return Ok(result);
        }



        // POST api/<GroupController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupCreationDto groupDto)
        {

            var id = User.Identity.GetId();

            var grupo = mapper.Map<Group>(groupDto);
            grupo.GroupUsers = new List<GroupUser> { new() { AppUserId = id } };
            grupo.CreatedBy = id;

            context.Add(grupo);

            #region Add users to group + Create Invitations
            groupDto.Emails.ForEach(async email =>
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    var groupUser = new GroupUser
                    {
                        AppUserId = user.Id,
                    };
                    grupo.GroupUsers.Add(groupUser);
                }
                else
                {
                    var invitation = new Invitation
                    {
                        Email = email,
                        Group = grupo,
                        CreatedBy = id,
                        InvitationStatus = InvitationStatus.PENDING
                    };
                    context.Invitations.Add(invitation);
                }
            });
            #endregion

            await context.SaveChangesAsync();
            var result = mapper.Map<GroupResponse>(grupo);

            return Ok(result);
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GroupCreationDto groupDto)
        {
            var groupFromDb = context.Groups
                .Include(group => group.GroupUsers)
                    .ThenInclude(groupUser => groupUser.AppUser)
                .Where(group => group.Id == id)
                .FirstOrDefault();

            if (groupFromDb == null)
            {
                return NotFound();
            }

            groupFromDb.Name = groupDto.Name;
            groupFromDb.Description = groupDto.Description;

            context.Update(groupFromDb);
            await context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPut("retrieve")]
        public async Task<IActionResult> JoinInvitedGroups()
        {
            var userId = User.Identity.GetId();
            var emailCurrentUser = User.Identity.GetEmail();

            var groupsInvited = await this.context.Invitations
                .Include(invitation => invitation.Group)
                .ThenInclude(group => group.GroupUsers)
                .Where(invitation => invitation.Email == emailCurrentUser && invitation.InvitationStatus == InvitationStatus.PENDING)
                .IgnoreQueryFilters()
                .Select(invitation => invitation.Group)
                .ToListAsync();

            var invitations = await context.Invitations.Include(x => x.Group).Where(i => i.Email == emailCurrentUser).IgnoreQueryFilters().ToListAsync();

            groupsInvited.ForEach(group =>
            {
                group.GroupUsers.Add(new GroupUser
                {
                    AppUserId = userId
                });
            });

            context.Invitations
                .Where(i => i.Email == emailCurrentUser)
                .ExecuteUpdate(i => i
                    .SetProperty(x => x.InvitationStatus, InvitationStatus.ACCEPTED)
                    .SetProperty(x =>x.AcceptedAt, DateTime.UtcNow)
                );
            
            await context.SaveChangesAsync();
            return Ok();
        }
    
    }

}
