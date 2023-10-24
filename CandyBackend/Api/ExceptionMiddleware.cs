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
            await HandleBadRequestExceptionAsync(httpContext, ex);
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Not found: {}", ex.Message);
            await HandleEntityNotFoundExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong: {}", ex);
            await HandleExceptionAsync(httpContext);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(new ErrorOut
        {
            Message = "Internal Server Error.",
        });
    }

    private async Task HandleEntityNotFoundExceptionAsync(HttpContext context, EntityNotFoundException exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        await context.Response.WriteAsJsonAsync(new ErrorOut
        {
            Message = exception.Message
        });
    }

    private async Task HandleBadRequestExceptionAsync(HttpContext context, BadRequestException exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await context.Response.WriteAsJsonAsync(new ErrorOut
        {
            Message = exception.Message,
        });
    }
}