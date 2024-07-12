using ApiGastos.Dtos;
using ApiGastos.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiGastos.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(IMapper mapper, ApplicationDbContext context, UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<AuthenticationResponse>> Registrar(UserCreationDTO userCreationDTO)
        {
            var usuario = mapper.Map<AppUser>(userCreationDTO);
            var result = await userManager.CreateAsync(usuario, userCreationDTO.Password);
            if(result.Succeeded)
            {
                return await BuildToken(new UserCredentialsDto { Email = userCreationDTO.Email, Password = userCreationDTO.Password});
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentialsDto creds)
        {
            var result = await signInManager.PasswordSignInAsync(creds.Email, creds.Password, isPersistent: false, lockoutOnFailure: false);
            if(result.Succeeded)
            {

                return await BuildToken(creds);
            }
            else
            {
                return BadRequest(new {error = "Contraseña o email incorrecto"});
            }

        }

        [HttpGet("refreshToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AuthenticationResponse>> refreshToken()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;


            var credentials = new UserCredentialsDto()
            {
                Email = email
            };

            var token = await BuildToken(credentials);
            return token;
        }


        private async Task<AuthenticationResponse> BuildToken(UserCredentialsDto credentials)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Email == credentials.Email);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email,credentials.Email),
            };

            #pragma warning disable CS8604 // Possible null reference argument.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecret"]));
            #pragma warning restore CS8604 // Possible null reference argument.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMonths(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);
            return new AuthenticationResponse()
            {
                Expiration = expiration,
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };
        }


    }
}
