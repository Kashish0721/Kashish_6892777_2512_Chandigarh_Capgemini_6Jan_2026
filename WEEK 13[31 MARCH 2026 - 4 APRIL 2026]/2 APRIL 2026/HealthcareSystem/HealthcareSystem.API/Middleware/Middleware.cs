using System.Net;
using System.Text.Json;
using HealthcareSystem.Models.DTOs;

namespace HealthcareSystem.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    { _next = next; _logger = logger; }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var (status, message) = ex switch
        {
            KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized access."),
            ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
            InvalidOperationException => (HttpStatusCode.BadRequest, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        context.Response.StatusCode = (int)status;

        var response = new ErrorResponse
        {
            StatusCode = (int)status,
            Message = message,
            Timestamp = DateTime.UtcNow,
            Details = context.RequestServices
                .GetService<IHostEnvironment>()?.IsDevelopment() == true
                ? ex.StackTrace : null
        };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        await context.Response.WriteAsync(json);
    }
}

// ─── Request Logging Middleware ───────────────────────────────────────────────
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    { _next = next; _logger = logger; }

    public async Task InvokeAsync(HttpContext context)
    {
        var start = DateTime.UtcNow;
        _logger.LogInformation("HTTP {Method} {Path} started at {Time}",
            context.Request.Method, context.Request.Path, start);

        await _next(context);

        var elapsed = (DateTime.UtcNow - start).TotalMilliseconds;
        _logger.LogInformation("HTTP {Method} {Path} completed {StatusCode} in {Elapsed}ms",
            context.Request.Method, context.Request.Path,
            context.Response.StatusCode, elapsed);
    }
}
