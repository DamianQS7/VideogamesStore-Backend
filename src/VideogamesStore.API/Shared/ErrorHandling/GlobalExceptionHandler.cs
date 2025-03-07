using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace VideogamesStore.API.Shared.ErrorHandling;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ActivityTraceId? traceId = Activity.Current?.TraceId;

        logger.LogError(
            exception, 
            "Could not process a request on machine {Machine}. TraceID: {TraceId}", 
            Environment.MachineName, traceId);

        await Results.Problem(
            title: "An error occurred while processing your request",
            statusCode: StatusCodes.Status500InternalServerError,
            extensions: new Dictionary<string, object?>
            {
                {"traceId", traceId?.ToString() }
            }
        ).ExecuteAsync(httpContext);

        return true; // Yes, we handled the exception
    }
}
