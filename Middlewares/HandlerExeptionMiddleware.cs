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
        catch (System.Exception e)
        {
            _logger.LogError(e.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var error = new
            {
                StatusCode = context.Response.StatusCode,
                Messafe = "Internal Server Error"
            };
            await context.Response.WriteAsJsonAsync(error);
        }
    }

}
/* _logger.LogInformation("Hi, Ini middleware");
 context.Request.Headers["Authorization"];
 await next(context);*/