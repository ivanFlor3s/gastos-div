using System.Text;
using Divtos.Infraestructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Divtos.Infraestructure.Security
{
    public sealed class JwtBearerTokenValidationConfiguration : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtSettings _jwtSettings;

        public JwtBearerTokenValidationConfiguration(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }


        public void Configure(string name, JwtBearerOptions options) => Configure(options);

        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // Inspeccionar el token recibido
                    var token = context.Token;
                    if (string.IsNullOrEmpty(token))
                    {
                        Console.WriteLine("Token not found in the request.");
                    }
                    else
                    {
                        Console.WriteLine($"Token received: {token}");
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    // Inspeccionar la excepción de autenticación
                    Console.WriteLine($"Authentication failed: {context.Exception?.Message}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    // Inspeccionar el usuario autenticado
                    Console.WriteLine($"Token validated successfully for user: {context.Principal?.Identity?.Name}");
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    // Inspeccionar por qué se emitió un challenge
                    Console.WriteLine($"Challenge triggered: {context.AuthenticateFailure?.Message}");
                    return Task.CompletedTask;
                }
            };
        }
    }
}
 