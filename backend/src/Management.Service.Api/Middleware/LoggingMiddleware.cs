namespace Management.Service.Api.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<LoggingMiddleware> logger)
    {
        logger.LogInformation("{time} | Started executing {method} endpoint", DateTime.Now, context.Request.Path);

        await _next.Invoke(context);
        
        logger.LogInformation("{time} | Ended executing {method} endpoint", DateTime.Now, context.Request.Path);
    }
}