using ApiGastos.Dtos;
using ApiGastos.Dtos.Responses;
using ApiGastos.Entities;
using ApiGastos.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private readonly UserManager<AppUser> _userManager;

        public GroupController(IMapper mapper, ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            this.mapper = mapper;
            this.context = context;
            _userManager = userManager;
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

            if(group == null)
            {
                return NotFound();
            }

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
            grupo.GroupUsers = new List<GroupUser> { new() { AppUserId = id, IsAdmin = true } };
            grupo.CreatedBy = id;

            context.Add(grupo);

            #region Add users to group + Create Invitations
            foreach (var email in groupDto.Emails)
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
                    var usuario = new UserCreationDTO
                    {
                        Email = email,
                        FirstName = "Guess",
                        LastName = "Temporal"
                    };
                    var userCreation = mapper.Map<AppUser>(usuario);
                    userCreation.IsTemporal = true;
                    var res = await _userManager.CreateAsync(userCreation, "$DumpTest123");
                    
                    if (res.Succeeded)
                    {
                        var groupUser = new GroupUser
                        {
                            AppUserId = userCreation.Id,
                        };
                        grupo.GroupUsers.Add(groupUser);
                    }   

                }
            }
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
        public async Task<IActionResult> Delete(int id)
        {
            var group = await context.Groups.Include(g => g.GroupUsers).FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                return NotFound("El grupo no existe.");
            }

            context.GroupUsers.RemoveRange(group.GroupUsers);

            context.Groups.Remove(group);

            await context.SaveChangesAsync();
            return Ok();    
        }

        
    
    }

}
