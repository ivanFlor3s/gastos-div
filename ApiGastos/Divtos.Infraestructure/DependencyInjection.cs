using Divtos.Application.Common.Interfaces.Authentication;
using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Infraestructure.Authentication;
using Divtos.Infraestructure.Common.Persistence;
using Divtos.Infraestructure.Users.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Divtos.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddPersistence();
            return services;
        }

       private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Host=localhost;Database=gastos_db;Port=5432;Username=postgres;Password=789963"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;    
        }
    }
}
