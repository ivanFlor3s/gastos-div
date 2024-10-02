using Divtos.Api.Filters;
using Divtos.Api.Middlewares;
using Divtos.Application;
using Divtos.Infraestructure;

namespace Divtos.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                // Add services to the container.
                builder.Services
                    .AddApplication()
                    .AddInfraestructure(builder.Configuration);

                builder.Services.AddControllers( opts => opts.Filters.Add<ErrorHandlingFilterAttribute>());
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }


            var app = builder.Build();
            {
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                //app.UseMiddleware<ErrorHandlingManager>();

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }

           
        }
    }
}
