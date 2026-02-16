using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TransaccionService.Api
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken ct
        )
        {
            var statusCode = exception switch
            {
                InvalidOperationException => StatusCodes.Status400BadRequest,
                HttpRequestException => StatusCodes.Status503ServiceUnavailable,
                _ => StatusCodes.Status500InternalServerError,
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = statusCode.ToString(),
                Detail = exception.Message,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);

            return true;
        }
    }
}
