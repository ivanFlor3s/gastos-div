using ApiGastos.Dtos.Spent;
using ApiGastos.Entities;
using ApiGastos.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
