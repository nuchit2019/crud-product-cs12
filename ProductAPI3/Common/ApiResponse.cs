namespace ProductAPI3.Common
{
    public class ApiResponse<T>(int statusCode, bool success, string? message = null, T? data = default, object? error = null)
    {
        public int StatusCode { get; init; } = statusCode;
        public bool Success { get; init; } = success;
        public string? Message { get; init; } = message;
        public T? Data { get; init; } = data;
        public object? Error { get; init; } = error;

        public static ApiResponse<T> SuccessResponse(T data, string? message = null, int statusCode = 200)  => new(statusCode, true, message, data, null);

        public static ApiResponse<T> ErrorResponse(string message, object? error = null, int statusCode = 400) => new(statusCode, false, message, default, error);
    }
}
