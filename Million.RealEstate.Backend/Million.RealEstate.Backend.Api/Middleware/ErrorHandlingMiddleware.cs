using Million.RealEstate.Backend.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace Million.RealEstate.Backend.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        string? errorCode = null;
        object? details = null;
        string errorMessage;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                errorMessage = "One or more validation errors occurred";
                // Convertir explícitamente el tipo
                details = ConvertValidationErrors(validationException.Errors);
                _logger.LogWarning(exception, "Validation error occurred");
                break;

            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                errorMessage = notFoundException.Message;
                errorCode = "NOT_FOUND";
                _logger.LogWarning(exception, "Resource not found: {Message}", notFoundException.Message);
                break;

            case BadRequestException badRequestException:
                code = HttpStatusCode.BadRequest;
                errorMessage = badRequestException.Message;
                errorCode = "BAD_REQUEST";
                details = badRequestException.AdditionalData;
                _logger.LogWarning(exception, "Bad request: {Message}", badRequestException.Message);
                break;

            case AppException appException:
                code = (HttpStatusCode)appException.StatusCode;
                errorMessage = appException.Message;
                errorCode = "APPLICATION_ERROR";
                details = appException.AdditionalData;
                _logger.LogWarning(exception, "Application error: {Message}", appException.Message);
                break;

            default:
                var errorId = Guid.NewGuid().ToString();
                errorMessage = _environment.IsDevelopment() ?
                    exception.Message : "An unexpected error occurred";
                errorCode = "INTERNAL_ERROR";
                details = new { errorId };
                _logger.LogError(exception, "Unhandled exception (ErrorId: {ErrorId})", errorId);
                break;
        }

        // Respuesta consistente
        var result = new
        {
            success = false,
            error = errorMessage,
            errorCode = errorCode,
            details = details,
            traceId = context.TraceIdentifier
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = _environment.IsDevelopment()
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
    }

    private static IDictionary<string, object?> ConvertValidationErrors(IDictionary<string, string[]> errors)
    {
        var result = new Dictionary<string, object?>();

        foreach (var error in errors)
        {
            result[error.Key] = error.Value;
        }

        return result;
    }
}
