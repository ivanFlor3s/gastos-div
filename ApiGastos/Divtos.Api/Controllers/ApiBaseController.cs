using Divtos.Api.Commons.Constants.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace DivtosApi.Controllers
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {

            HttpContext.Items[HttpContextItemKeys.Errors] = errors;

            var firstError = errors.First();
            var statusCode = firstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                title: firstError.Description,
                statusCode: statusCode);
        }
    }
}
