
using System.CodeDom;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;

public class ExceptionMiddle : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
       try
       {
           await next(context);
       }
       catch(ValidationException e)
       {
           await HandleValidationException(context, e);
       }
    //    catch (Exception e)
    //    {
           
    //    }
    }

    private static  async Task HandleValidationException(HttpContext context, ValidationException e)
    {
        var validationError = new Dictionary<string, string[]>();
        if(e.Errors != null)
        {
            foreach (var error in e.Errors)
            {
               if(validationError.TryGetValue(error.PropertyName, out var existingError)){
                     validationError[error.PropertyName] = [.. existingError, error.ErrorMessage];
                }
                else
                {
                     validationError.Add(error.PropertyName, [error.ErrorMessage]);
               }
            }
        }
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var validationProblemDetails = new ValidationProblemDetails(validationError)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "ValidationFailure",
            Title = "Validation errors occurred",
            Detail = "One or more validation errors occurred"

        };

        await context.Response.WriteAsJsonAsync(validationProblemDetails);
    }
}
