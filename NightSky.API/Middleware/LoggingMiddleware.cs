using NightSky.API.Constants;
using NightSky.API.Exceptions;

namespace NightSky.API.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseNightSkyException e)
        {
            _logger.LogError(e, "Night sky exception");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(e.Errors);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Uncaught exception");
            var errors = new ValidationErrorBuilder()
                .AddError(Langs.English, "Internal server error. Contact administrator")
                .AddError(Langs.Polish, "Błąd wewnętrzny. Skontaktuj się z administratorem.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(errors.Build());
        }
    }
}