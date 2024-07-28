using ApiGastos.Dtos.Responses;
using ApiGastos.Dtos.Spent;
using ApiGastos.Entities;
using ApiGastos.Helpers;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGastos.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/groups/{groupId}/spents")]

    public class SpentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public SpentsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(int groupId, [FromBody] AddSpentDto spentDto)
        {
            var group = await context.Groups.FindAsync(groupId);
            if(group == null)
            {
                return NotFound();
            }

            group.Spents ??= new List<Spent>();    

            var spent = mapper.Map<Spent>(spentDto);
            spent.Participants = spentDto.Participants
                .Select(p => new SpentParticipant
                    {
                        UserId = p.Value
                    }).ToList();

            spent.CreatedBy = User.Identity.GetId();

            group.Spents.Add(spent);
            
            await context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpDelete("{spentId}")]
        public async Task<IActionResult> Delete(int groupId, int spentId)
        {

            var group = await context.Groups.FindAsync(groupId);
            if (group == null)
            {
                return NotFound();
            }

            var spent = await context.Spent.FindAsync(spentId);
            if(spent == null)
            {
                return NotFound();
            }

            context.Spent.Remove(spent);
            
            await context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpGet("{spentId}")]
        public async Task<IActionResult> Get(int groupId,int spentId) { 

            var group = await context.Groups.FindAsync(groupId);
            if (group == null)
            {
                return NotFound();
            }

            var spent = await context.Spent
                .Include(spent => spent.Author)
                .Include(spent => spent.Participants)
                .ThenInclude(participant => participant.User)
                .FirstOrDefaultAsync(spent => spent.Id == spentId);
               
            if (spent == null)
            {
                return NotFound();
            }

            var result = mapper.Map<SpentResponse>(spent);

            return Ok(result);
        }
    }
}
