using Newtonsoft.Json;
using System.Net;

namespace Divtos.Api.Middlewares
{
    public class ErrorHandlingManager
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingManager(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new {error = exception.Message});

            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }   

    }
}
