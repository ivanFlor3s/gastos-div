using Divtos.Application.Services.Authentication;
using Divtos.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Divtos.Api.Controllers
{
    [ApiController]
    [Route("api/v2/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
            return Ok(authResult);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(request.Email, request.Password);
            return Ok(authResult);
        }   
    }
}
