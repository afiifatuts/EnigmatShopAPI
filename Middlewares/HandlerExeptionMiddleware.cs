using System.Net;
using EnigmaShopApi.Exceptions;
using EnigmaShopApi.Dto;

namespace EnigmaShopApi.Middlewares;

public class HandleExceptionMiddleware : IMiddleware
{
    private readonly ILogger<HandleExceptionMiddleware> _logger;

    public HandleExceptionMiddleware(ILogger<HandleExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(context, e);
        }

        catch (System.Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = new Error
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal server error"
        };

        switch (exception)
        {
            case NotFoundException:
                error.StatusCode = (int)HttpStatusCode.NotFound;
                error.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case not null:
                error.StatusCode = (int)HttpStatusCode.InternalServerError;
                error.Message = exception.Message;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }
        await context.Response.WriteAsJsonAsync(error);
    }

}
/* _logger.LogInformation("Hi, Ini middleware");
 context.Request.Headers["Authorization"];
 await next(context);*/