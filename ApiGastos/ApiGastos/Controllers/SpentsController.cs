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
    [Route("api/spents")]
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
        public async Task<IActionResult> Post([FromBody] AddSpentDto spentDto)
        {
            var group = await context.Groups.FindAsync(spentDto.GroupId);
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
    }
}
