using InnoClinic.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace InnoClinic.Authorization.Middlewares;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        
        var excDetails = exception switch
        {
            ValidationAppException => (Detail: exception.Message, StatusCode: StatusCodes.Status422UnprocessableEntity),
            _ => (Detail: exception.Message, StatusCode: StatusCodes.Status500InternalServerError)
        };
        
        httpContext.Response.StatusCode = excDetails.StatusCode;

        if (exception is ValidationAppException validationAppException)
        {
            await httpContext.Response.WriteAsJsonAsync(new { validationAppException.Errors });
            return true;
        }
        
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = "An error occurred",
                Detail = excDetails.Detail,
                Type = exception.GetType().Name,
                Status = excDetails.StatusCode,
            },
            Exception = exception
        });
    }
}