namespace TaskMaster.Api.Middleware;

/// <summary>
/// Preview of "central exception handling" (the slides introduce this later).
/// It turns ArgumentException thrown by the service (a broken business rule) into HTTP 400,
/// so the controller does not need try/catch around every call.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (ArgumentException ex)
        {
            // Expected problem: a business rule was broken -> 400 Bad Request
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (OperationCanceledException)
        {
            // The client closed the connection; nothing to report.
            // 499 is the de-facto "client closed request" code.
            context.Response.StatusCode = 499;
        }
        catch (Exception ex)
        {
            // Unexpected problem: log it, but do not leak internals to the client.
            _logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occurred." });
        }
    }
}
