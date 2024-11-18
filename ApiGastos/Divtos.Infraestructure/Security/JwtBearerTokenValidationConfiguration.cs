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
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        context.Response.StatusCode = 401; // Unauthorized
                        context.Response.ContentType = "application/json";

                        // Mensaje detallado
                        var result = System.Text.Json.JsonSerializer.Serialize(new
                        {
                            error = "Token expired",
                            message = "The provided token has expired. Please login again."
                        });

                        return context.Response.WriteAsync(result);
                    }

                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    context.Response.ContentType = "application/json";

                    var result = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        error = "Invalid token",
                        message = "Authentication failed due to an invalid or missing token."
                    });

                    return context.Response.WriteAsync(result);
                }
            };
        }
    }
}
