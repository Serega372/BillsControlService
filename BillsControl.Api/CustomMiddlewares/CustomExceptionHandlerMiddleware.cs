using BillsControl.Api.Dtos;
using BillsControl.ApplicationCore.CustomExceptions;

namespace BillsControl.Api.CustomMiddlewares;

public class CustomExceptionHandlerMiddleware(
    RequestDelegate next, 
    ILogger<CustomExceptionHandlerMiddleware> logger,
    IWebHostEnvironment environment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = ex switch
            {
                BillNotFoundException => StatusCodes.Status404NotFound,
                BillIsClosedException => StatusCodes.Status400BadRequest,
                BillAlreadyClosedException => StatusCodes.Status409Conflict,
                ResidentNotFoundException => StatusCodes.Status404NotFound,
                InvalidCloseDateInBillException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";

            var response = new ErrorResponse(
                context.Response.StatusCode,
                ex.Message,
                environment.IsDevelopment() ? ex.StackTrace : null);
            
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}