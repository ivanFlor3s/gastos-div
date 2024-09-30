using Divtos.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Divtos.Infraestructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(string userId, string email, string name, string lastname, DateTime expires)
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key-this-is-longer-now"));
            #pragma warning restore CS8604 // Possible null reference argument.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, name),
                new(ClaimTypes.Surname, lastname),
                new(ClaimTypes.Email,email),
            };

            var securityToken = new JwtSecurityToken(issuer: "Divtos", audience: null, claims: claims,
                               expires: expires, signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }
    }
}
