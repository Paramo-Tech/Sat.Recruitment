using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Middlewares;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Domain.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
        catch (Exception ex) when 
            (ex is DomainException || 
            ex is ValidationException ||
            ex is RepeatedUserException)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode responseCode)
    {
        _logger.LogError($"An error occurred while processing the request. Details: {exception}");

        var result = JsonSerializer.Serialize(new ErrorResponse(exception.Message));

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)responseCode;

        await context.Response.WriteAsync(result);
    }
}