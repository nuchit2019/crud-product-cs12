using ProductAPI3.Common;
using System.Text.Json;

namespace ProductAPI3.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var error = new
                {
                    exception = ex.GetType().Name,
                    details = ex.Message
                };

                var response = ApiResponse<object>.ErrorResponse(
                    message: "Internal Server Error",
                    error: error,
                    statusCode: 500
                );

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
