using System;
using System.Diagnostics;

namespace VideogamesStore.API.Shared.Timing;

public class RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        try
        {
            stopwatch.Start();

            await next(context);
        }
        finally
        {
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;

            logger.LogInformation(
                "{RequestMethod} {RequestPath} completed in {elapsed}ms", 
                context.Request.Method,
                context.Request.Path, 
                elapsed);
        }
    }
}
