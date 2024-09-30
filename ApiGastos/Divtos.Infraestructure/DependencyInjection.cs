using Divtos.Application.Common.Interfaces.Authentication;
using Divtos.Infraestructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Divtos.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            return services;
        }
    }
}
