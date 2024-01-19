using System.Net;
using CandyBackend.Api.Dto;
using CandyBackend.Core.Exceptions;

namespace CandyBackend.Api;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BadRequestException ex)
        {
            await WriteResponse(httpContext, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Not found: {}", ex.Message);
            await WriteResponse(httpContext, HttpStatusCode.NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong:");
            await WriteResponse(httpContext, HttpStatusCode.InternalServerError, "Internal Server Error.");
        }
    }

    private async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(new ErrorOut
        {
            Message = message,
        });
    }
}