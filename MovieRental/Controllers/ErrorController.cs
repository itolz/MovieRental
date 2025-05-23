using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MovieRental.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        public IActionResult ErrorHandler()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;


            return Problem(
                detail: exception?.Message,
                statusCode: 500,
                title: "Error managed by ErrorController"
            );
        }

        [HttpGet("ErrorHandlerHealthCheck")]
        public IActionResult ErrorHandlerHealthCheck()
        {
            throw new Exception("ErrorHandler Health Check Test Exception.");
        }
    }
}
