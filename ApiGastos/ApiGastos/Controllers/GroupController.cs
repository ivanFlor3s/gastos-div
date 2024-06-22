using ApiGastos.Dtos;
using ApiGastos.Dtos.Responses;
using ApiGastos.Entities;
using ApiGastos.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
                .ToListAsync();
            return mapper.Map<List<GroupResponse>>(groups);   
        }

        // GET api/<GroupController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var group = await this.context.Groups
                .Include(groupDb => groupDb.GroupUsers)
                .ThenInclude(groupUserDb => groupUserDb.AppUser)
                .FirstOrDefaultAsync(groupDb => groupDb.Id == id);

            var result = mapper.Map<GroupResponse>(group);

            return Ok(result);
        }

        // POST api/<GroupController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupCreationDto groupDto)
        {

            var id = User.Identity.GetId();

            var grupo = mapper.Map<Group>(groupDto);
            grupo.GroupUsers = new List<GroupUser> { new GroupUser { AppUserId = id  } };
            grupo.CreatedBy = id;
            
            context.Add(grupo);
            await context.SaveChangesAsync();

            var result = mapper.Map<GroupResponse>(grupo);

            return Ok(result);
        }

        // PUT api/<GroupController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GroupController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
