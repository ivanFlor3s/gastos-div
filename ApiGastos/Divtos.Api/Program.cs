using Divtos.Api.Errors;
using Divtos.Api.Filters;
using Divtos.Api.Middlewares;
using Divtos.Application;
using Divtos.Infraestructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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

                builder.Services.AddSingleton<ProblemDetailsFactory, DivtosApiProblemDetailsFactory>();

                //builder.Services.AddControllers( opts => opts.Filters.Add<ErrorHandlingFilterAttribute>());
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }


            var app = builder.Build();
            {
                app.UseExceptionHandler("/error");
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
