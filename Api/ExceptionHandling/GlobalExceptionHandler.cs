using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.ExceptionHandling;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var (title, statusCode) = exception switch
        {
            DomainException => ("Domain validation exception.", StatusCodes.Status400BadRequest),
            _ => ("An error occured.", StatusCodes.Status500InternalServerError)
        };

        var problemDetails = new ProblemDetails()
        {
            Title = title,
            Detail = exception.Message,
            Instance = httpContext.Request.Path,
            Status = statusCode
        };

        httpContext.Response.StatusCode = statusCode;
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}