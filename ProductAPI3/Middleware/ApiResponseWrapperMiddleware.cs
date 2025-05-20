using ProductAPI3.Common;
using System.Text.Json;

namespace ProductAPI3.Middleware;

public class ApiResponseWrapperMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
            await _next(context);
            return;
        }

        var originalBodyStream = context.Response.Body;
        await using var newBody = new MemoryStream();
        context.Response.Body = newBody;

        try
        {
            await _next(context); // ถ้ามี Exception → jump ไป catch (ดีแล้ว)
        }
        catch
        {
            // สำคัญ: ปล่อยให้ ExceptionHandlingMiddleware จัดการ
            context.Response.Body = originalBodyStream;
            throw;
        }

        // ถ้าไม่มี Exception ค่อย wrap
        newBody.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(newBody).ReadToEndAsync();
        context.Response.Body = originalBodyStream;

        if (context.Response.HasStarted) return;

        var statusCode = context.Response.StatusCode;
        context.Response.ContentType = "application/json";

        string json;

        if (statusCode >= 200 && statusCode < 300)
        {
            var wrapped = ApiResponse<object>.SuccessResponse(
                data: JsonSerializer.Deserialize<object>(responseBody),
                message: "OK",
                statusCode: statusCode
            );
            json = JsonSerializer.Serialize(wrapped);
            await context.Response.WriteAsync(json);
        }
        else
        {
            // ถ้า status != 2xx → ห้ามเขียนซ้ำ (อาจมี ErrorHandler เขียนไปแล้ว)
            await context.Response.WriteAsync(responseBody);
        }
    }


}

